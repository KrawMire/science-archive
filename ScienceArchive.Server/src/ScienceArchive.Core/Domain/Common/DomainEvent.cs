using MediatR;

namespace ScienceArchive.Core.Domain.Common;

/// <summary>
/// Base class representing a domain event.
/// </summary>
public abstract record DomainEvent : INotification
{
    /// <summary>
    /// The timestamp of the domain event
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.Now;
}