namespace ScienceArchive.Infrastructure.Connectivity.RabbitMq.Options;

public class RabbitMqConnectionOptions
{
    public string? Host { get; set; }
    public string? ConnectionString { get; set; }
    public required string NotificationsQueueName { get; set; }
    public required string RequestLogsQueueName { get; set; }
}