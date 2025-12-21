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

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var queueName = configuration.GetValue("RabbitMq:QueueName", "rental-log-create")!;

        logger.LogInformation("RentalLogConsumer starting with QueueName={QueueName}...", queueName);

        _channel = await connection.CreateChannelAsync(cancellationToken: stoppingToken);

        await _channel.QueueDeclareAsync(
            queue: queueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null,
            cancellationToken: stoppingToken);

        await _channel.BasicQosAsync(prefetchSize: 0, prefetchCount: 1, global: false, cancellationToken: stoppingToken);

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

                await _channel.BasicAckAsync(ea.DeliveryTag, multiple: false, stoppingToken);
            }
            catch (KeyNotFoundException ex)
            {
                logger.LogWarning("Validation failed, skipping message: {Error}", ex.Message);
                await _channel.BasicAckAsync(ea.DeliveryTag, multiple: false, stoppingToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error processing message");
                await _channel.BasicNackAsync(ea.DeliveryTag, multiple: false, requeue: true, cancellationToken: stoppingToken);
            }
        };

        await _channel.BasicConsumeAsync(
            queue: queueName,
            autoAck: false,
            consumer: consumer,
            cancellationToken: stoppingToken);

        logger.LogInformation("RentalLogConsumer started, listening for messages...");
    }

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