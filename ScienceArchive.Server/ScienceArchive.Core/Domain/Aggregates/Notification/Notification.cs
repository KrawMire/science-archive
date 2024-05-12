using ScienceArchive.Core.Domain.Aggregates.Notification.ValueObjects;
using ScienceArchive.Core.Domain.Common;

namespace ScienceArchive.Core.Domain.Aggregates.Notification;

public class Notification : AggregateRoot<NotificationId>
{
    public Notification(NotificationId id) : base(id)
    {
    }
    
    public required string Receiver { get; set; }
    public required string Message { get; set; }
}