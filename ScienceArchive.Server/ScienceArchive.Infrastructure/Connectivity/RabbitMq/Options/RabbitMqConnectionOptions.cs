namespace ScienceArchive.Infrastructure.Connectivity.RabbitMq.Options;

public class RabbitMqConnectionOptions
{
    public required string Host { get; set; }
    public required string NotificationsQueueName { get; set; }
    public required string RequestLogsQueueName { get; set; }
}