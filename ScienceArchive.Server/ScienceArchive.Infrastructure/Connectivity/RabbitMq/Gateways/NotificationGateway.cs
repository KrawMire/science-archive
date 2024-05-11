using System.Text.Json;
using RabbitMQ.Client;
using ScienceArchive.Core.Domain.Aggregates.Notification;
using ScienceArchive.Core.Gateways;
using ScienceArchive.Infrastructure.Connectivity.RabbitMq.Options;

namespace ScienceArchive.Infrastructure.Connectivity.RabbitMq.Gateways;

internal class RabbitMqNotificationGateway : INotificationGateway
{
    private readonly string _queueName;
    private readonly string _host;

    public RabbitMqNotificationGateway(RabbitMqConnectionOptions options)
    {
        _host = options.Host;
        _queueName = options.NotificationsQueueName;
    }

    public async Task SendNotification(Notification notification)
    {
        var factory = new ConnectionFactory
        {
            HostName = _host
        };

        using var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(
            queue: _queueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var body = JsonSerializer.SerializeToUtf8Bytes(notification);
        
        await channel.BasicPublishAsync(
            exchange: string.Empty,
            routingKey: _queueName,
            body: body);
    }
}