namespace ScienceArchive.Infrastructure.Connectivity.RabbitMq.Models;

internal class NotificationModel
{
    public required NotificationTargetService TargetService { get; set; }
    public required short Type { get; set; }
    public required string MessageTitle { get; set; }
    public required string Message { get; set; }
    public required string Recipient { get; set; }
}