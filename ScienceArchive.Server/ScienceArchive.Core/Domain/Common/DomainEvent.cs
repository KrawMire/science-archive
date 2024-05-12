namespace ScienceArchive.Core.Domain.Common;

/// <summary>
/// Base class representing a domain event.
/// </summary>
public abstract class DomainEvent
{
    /// <summary>
    /// The timestamp of the domain event
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.Now;
}