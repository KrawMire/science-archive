using System.Text.Json;
using RabbitMQ.Client;
using ScienceArchive.Core.Domain.Aggregates.Notification;
using ScienceArchive.Core.Gateways;
using ScienceArchive.Infrastructure.Connectivity.RabbitMq.Models;
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
        var notificationModel = GetNotificationModel(notification);
        
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

        var body = JsonSerializer.SerializeToUtf8Bytes(notificationModel);
        
        await channel.BasicPublishAsync(
            exchange: string.Empty,
            routingKey: _queueName,
            body: body);
    }

    private NotificationModel GetNotificationModel(Notification notification)
    {
        return new NotificationModel
        {
            Message = notification.Message,
            MessageTitle = "Test message",
            Recipient = notification.Receiver,
            Type = 0,
            TargetService = NotificationTargetService.Email
        };
    }
}