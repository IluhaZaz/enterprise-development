using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace CarRental.Generator;

/// <summary>
/// Publishes messages to RabbitMQ
/// </summary>
public class RabbitMqPublisher(
    IConnection connection,
    ILogger<RabbitMqPublisher> logger,
    IOptions<GeneratorOptions> options) : IAsyncDisposable
{
    private readonly GeneratorOptions _options = options.Value;
    private const int MaxRetries = 10;

    private IChannel? _channel;

    /// <summary>
    /// Initializes the connection to RabbitMQ with retry logic
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if connection was successful, false otherwise</returns>
    public async Task<bool> InitializeAsync(CancellationToken cancellationToken = default)
    {
        var retryCount = 0;

        while (_channel == null && !cancellationToken.IsCancellationRequested)
        {
            try
            {
                _channel = await connection.CreateChannelAsync(cancellationToken: cancellationToken);

                await _channel.QueueDeclareAsync(
                    queue: _options.QueueName,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null,
                    cancellationToken: cancellationToken);

                logger.LogInformation("Successfully connected to RabbitMQ and declared queue '{QueueName}'", _options.QueueName);
                return true;
            }
            catch (Exception ex)
            {
                retryCount++;
                if (retryCount >= MaxRetries)
                {
                    logger.LogError(ex, "Failed to connect to RabbitMQ after {MaxRetries} attempts", MaxRetries);
                    return false;
                }

                logger.LogWarning(ex, "Failed to connect to RabbitMQ, retry {RetryCount}/{MaxRetries} in 5 seconds...", retryCount, MaxRetries);
                await Task.Delay(5000, cancellationToken);
            }
        }

        return false;
    }

    /// <summary>
    /// Publishes a message to RabbitMQ
    /// </summary>
    /// <typeparam name="T">Type of the message</typeparam>
    /// <param name="message">Message to publish</param>
    /// <param name="cancellationToken">Cancellation token</param>
    public async Task PublishAsync<T>(T message, CancellationToken cancellationToken = default)
    {
        if (_channel == null)
        {
            throw new InvalidOperationException("Publisher is not initialized. Call InitializeAsync first.");
        }

        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);

        await _channel.BasicPublishAsync(
            exchange: string.Empty,
            routingKey: _options.QueueName,
            mandatory: false,
            body: body,
            cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Closes the RabbitMQ channel
    /// </summary>
    public async ValueTask DisposeAsync()
    {
        if (_channel != null)
        {
            await _channel.CloseAsync();
            _channel = null;
        }
    }
}