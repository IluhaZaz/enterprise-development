using Microsoft.Extensions.Options;

namespace CarRental.Generator;

/// <summary>
/// Background worker that orchestrates rental log generation and publishing
/// </summary>
public class GeneratorWorker(
    RentalLogGenerator generator,
    RabbitMqPublisher publisher,
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

        if (!await publisher.InitializeAsync(stoppingToken))
        {
            logger.LogError("Failed to initialize message publisher. Stopping worker.");
            return;
        }

        while (!stoppingToken.IsCancellationRequested)
        {
            await PublishBatchAsync(stoppingToken);
            await Task.Delay(_options.IntervalMs, stoppingToken);
        }

        await publisher.DisposeAsync();
    }

    /// <summary>
    /// Generating a message and publishing it through a producer
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    private async Task PublishBatchAsync(CancellationToken cancellationToken)
    {
        try
        {
            var batch = generator.Generate(_options.BatchSize);

            foreach (var rentalLog in batch)
            {
                await publisher.PublishAsync(rentalLog, cancellationToken);

                logger.LogInformation(
                    "Published RentalLogCreate: CarId={CarId}, ClientId={ClientId}, StartDate={StartDate}, Duration={Duration}",
                    rentalLog.CarId, rentalLog.ClientId, rentalLog.RentStartDate, rentalLog.Duration);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error publishing message batch");
        }
    }
}