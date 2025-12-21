using System.Text;
using System.Text.Json;
using Bogus;
using CarRental.Application.Contracts;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace CarRental.Generator;

/// <summary>
/// Background worker that generates random RentalLogCreate messages and publishes them to RabbitMQ
/// </summary>
public class GeneratorWorker(
    IConnection connection,
    ILogger<GeneratorWorker> logger,
    IOptions<GeneratorOptions> options) : BackgroundService
{
    private readonly GeneratorOptions _options = options.Value;

    /// <summary>
    /// Executes the background task to generate and publish messages
    /// </summary>
    /// <param name="stoppingToken">A <see cref="CancellationToken"/> that can be used to stop the background service</param>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation(
            "Generator starting with IntervalMs={IntervalMs}, BatchSize={BatchSize}, MaxCarId={MaxCarId}, MaxClientId={MaxClientId}, QueueName={QueueName}",
            _options.IntervalMs, _options.BatchSize, _options.MaxCarId, _options.MaxClientId, _options.QueueName);

        var faker = new Faker<RentalLogCreate>()
            .RuleFor(r => r.CarId, f => f.Random.Int(1, _options.MaxCarId))
            .RuleFor(r => r.ClientId, f => f.Random.Int(1, _options.MaxClientId))
            .RuleFor(r => r.RentStartDate, f => f.Date.Between(DateTime.UtcNow.AddDays(-30), DateTime.UtcNow.AddDays(30)))
            .RuleFor(r => r.Duration, f => f.Random.Double(1, 30));

        IChannel? channel = null;
        var retryCount = 0;
        const int maxRetries = 10;

        while (channel == null && !stoppingToken.IsCancellationRequested)
        {
            try
            {
                channel = await connection.CreateChannelAsync(cancellationToken: stoppingToken);

                await channel.QueueDeclareAsync(
                    queue: _options.QueueName,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null,
                    cancellationToken: stoppingToken);

                logger.LogInformation("Successfully connected to RabbitMQ and declared queue");
            }
            catch (Exception ex)
            {
                retryCount++;
                if (retryCount >= maxRetries)
                {
                    logger.LogError(ex, "Failed to connect to RabbitMQ after {MaxRetries} attempts", maxRetries);
                    throw;
                }

                logger.LogWarning(ex, "Failed to connect to RabbitMQ, retry {RetryCount}/{MaxRetries} in 5 seconds...", retryCount, maxRetries);
                await Task.Delay(5000, stoppingToken);
            }
        }

        if (channel == null)
        {
            return;
        }

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                for (var i = 0; i < _options.BatchSize; i++)
                {
                    var rentalLog = faker.Generate();
                    var message = JsonSerializer.Serialize(rentalLog);
                    var body = Encoding.UTF8.GetBytes(message);

                    await channel.BasicPublishAsync(
                        exchange: string.Empty,
                        routingKey: _options.QueueName,
                        mandatory: false,
                        body: body,
                        cancellationToken: stoppingToken);

                    logger.LogInformation(
                        "Published RentalLogCreate: CarId={CarId}, ClientId={ClientId}, StartDate={StartDate}, Duration={Duration}",
                        rentalLog.CarId, rentalLog.ClientId, rentalLog.RentStartDate, rentalLog.Duration);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error publishing message");
            }

            await Task.Delay(_options.IntervalMs, stoppingToken);
        }

        await channel.CloseAsync(stoppingToken);
    }
}