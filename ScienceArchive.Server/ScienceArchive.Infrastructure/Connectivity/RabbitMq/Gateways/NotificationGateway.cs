using RabbitMQ.Client;
using ScienceArchive.Core.Domain.Aggregates.Notification;
using ScienceArchive.Core.Gateways;

namespace ScienceArchive.Infrastructure.Connectivity.RabbitMq.Gateways;

public class RabbitMqNotificationGateway : INotificationGateway
{
    public async Task SendNotification(Notification notification)
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost",
        };

        using var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(
            queue: "notifications",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
        
        // TODO: Create body
        await channel.BasicPublishAsync(
            exchange: string.Empty,
            routingKey: "notifications",
            body: null);
    }
}