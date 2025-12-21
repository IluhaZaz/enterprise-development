namespace CarRental.Generator;

/// <summary>
/// Defines configuration options for the <see cref="GeneratorWorker"/>
/// </summary>
public class GeneratorOptions
{
    /// <summary>
    /// Name of the configuration section
    /// </summary>
    public const string SectionName = "Generator";

    /// <summary>
    /// Interval in milliseconds between message generation batches
    /// </summary>
    public int IntervalMs { get; set; } = 5000;

    /// <summary>
    /// Number of messages to generate in a single batch
    /// </summary>
    public int BatchSize { get; set; } = 1;

    /// <summary>
    /// Maximum car ID for random generation
    /// </summary>
    public int MaxCarId { get; set; } = 28;

    /// <summary>
    /// Maximum client ID for random generation
    /// </summary>
    public int MaxClientId { get; set; } = 20;

    /// <summary>
    /// Name of the RabbitMQ queue to publish messages to
    /// </summary>
    public string QueueName { get; set; } = "rental-log-create";
}