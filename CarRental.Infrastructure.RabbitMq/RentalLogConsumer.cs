using System.Text;
using System.Text.Json;
using CarRental.Application.Contracts;
using CarRental.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace CarRental.Infrastructure.RabbitMq;

/// <summary>
/// Background service that consumes RentalLogCreate messages from RabbitMQ queue
/// </summary>
public class RentalLogConsumer(
    IServiceScopeFactory scopeFactory,
    IConnection connection,
    IConfiguration configuration,
    ILogger<RentalLogConsumer> logger) : BackgroundService
{
    private IChannel? _channel;

    /// <summary>
    /// Executes the background task to consume and process messages from RabbitMQ
    /// </summary>
    /// <param name="stoppingToken">A <see cref="CancellationToken"/> that can be used to stop the background service</param>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var queueName = configuration.GetValue("RabbitMq:QueueName", "rental-log-create")!;

        logger.LogInformation("RentalLogConsumer starting with QueueName={QueueName}...", queueName);

        var retryCount = 0;
        const int maxRetries = 10;

        while (_channel == null && !stoppingToken.IsCancellationRequested)
        {
            try
            {
                _channel = await connection.CreateChannelAsync(cancellationToken: stoppingToken);

                await _channel.QueueDeclareAsync(
                    queue: queueName,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null,
                    cancellationToken: stoppingToken);

                await _channel.BasicQosAsync(prefetchSize: 0, prefetchCount: 1, global: false, cancellationToken: stoppingToken);

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

        if (_channel == null)
        {
            return;
        }

        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.ReceivedAsync += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            logger.LogInformation("Received message: {Message}", message);

            try
            {
                var rentalLogCreate = JsonSerializer.Deserialize<RentalLogCreate>(message);

                if (rentalLogCreate != null)
                {
                    using var scope = scopeFactory.CreateScope();
                    var rentalLogService = scope.ServiceProvider.GetRequiredService<RentalLogService>();

                    var id = await rentalLogService.Create(rentalLogCreate);
                    logger.LogInformation("Successfully created RentalLog with ID: {Id}", id);
                }

                await _channel.BasicAckAsync(ea.DeliveryTag, multiple: false);
            }
            catch (KeyNotFoundException ex)
            {
                logger.LogWarning("Validation failed, skipping message: {Error}", ex.Message);
                await _channel.BasicAckAsync(ea.DeliveryTag, multiple: false);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error processing message");
                await _channel.BasicNackAsync(ea.DeliveryTag, multiple: false, requeue: true);
            }
        };

        await _channel.BasicConsumeAsync(
            queue: queueName,
            autoAck: false,
            consumer: consumer,
            cancellationToken: stoppingToken);

        logger.LogInformation("RentalLogConsumer started, listening for messages...");
    }

    /// <summary>
    /// Stops the background service and closes the RabbitMQ channel
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to stop the background service</param>
    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("RentalLogConsumer stopping...");

        if (_channel != null)
        {
            await _channel.CloseAsync(cancellationToken);
        }

        await base.StopAsync(cancellationToken);
    }
}