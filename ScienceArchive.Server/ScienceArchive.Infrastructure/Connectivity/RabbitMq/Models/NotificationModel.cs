namespace ScienceArchive.Infrastructure.Connectivity.RabbitMq.Models;

public class NotificationModel
{
    public required string TargetService { get; set; }
    public required string Type { get; set; }
    public required string MessageTitle { get; set; }
    public required string Message { get; set; }
    public required string Recipient { get; set; }
}