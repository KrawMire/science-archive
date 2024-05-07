using ScienceArchive.Core.Domain.Common;

namespace ScienceArchive.Core.Domain.Aggregates.Notification;

public class Notification : AggregateRoot<Guid>
{
    public Notification(Guid id) : base(id)
    {
    }
    
    public required string Receiver { get; set; }
    public required string Message { get; set; }
}