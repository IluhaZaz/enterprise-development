using System.Text;
using System.Text.Json;
using Bogus;
using CarRental.Application.Contracts;
using RabbitMQ.Client;

namespace CarRental.Generator;

/// <summary>
/// Background worker that generates random RentalLogCreate messages and publishes them to RabbitMQ
/// </summary>
public class GeneratorWorker(
    IConnection connection,
    ILogger<GeneratorWorker> logger,
    IConfiguration configuration) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var intervalMs = configuration.GetValue("Generator:IntervalMs", 5000);
        var batchSize = configuration.GetValue("Generator:BatchSize", 2);
        var maxCarId = configuration.GetValue("Generator:MaxCarId", 28);
        var maxClientId = configuration.GetValue("Generator:MaxClientId", 20);
        var queueName = configuration.GetValue("Generator:QueueName", "rental-log-create")!;

        logger.LogInformation(
            "Generator starting with IntervalMs={IntervalMs}, BatchSize={BatchSize}, MaxCarId={MaxCarId}, MaxClientId={MaxClientId}, QueueName={QueueName}",
            intervalMs, batchSize, maxCarId, maxClientId, queueName);

        var faker = new Faker<RentalLogCreate>()
            .RuleFor(r => r.CarId, f => f.Random.Int(1, maxCarId))
            .RuleFor(r => r.ClientId, f => f.Random.Int(1, maxClientId))
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
                    queue: queueName,
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
                for (var i = 0; i < batchSize; i++)
                {
                    var rentalLog = faker.Generate();
                    var message = JsonSerializer.Serialize(rentalLog);
                    var body = Encoding.UTF8.GetBytes(message);

                    await channel.BasicPublishAsync(
                        exchange: string.Empty,
                        routingKey: queueName,
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

            await Task.Delay(intervalMs, stoppingToken);
        }

        await channel.CloseAsync(stoppingToken);
    }
}