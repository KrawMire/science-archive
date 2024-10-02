using ScienceArchive.Core.Domain.Common.Identifiers;

namespace ScienceArchive.Core.Domain.Common;

/// <summary>
/// Represents an interface for the domain event bus.
/// </summary>
public interface IDomainEventBus
{
    /// <summary>
    /// Handles events in the event bus.
    /// </summary>
    Task HandleEvents();

    /// <summary>
    /// Adds an event to the event bus.
    /// </summary>
    /// <param name="domainEvent">The event to be added.</param>
    public void AddEvent<T>(T domainEvent) where T : DomainEvent;

    /// <summary>
    /// Adds multiple events to the event bus.
    /// </summary>
    /// <param name="domainEvents">The collection of events to be added.</param>
    /// <typeparam name="T">The type of the events being added. Must derive from DomainEvent.</typeparam>
    public void AddEvents<T>(IEnumerable<T> domainEvents) where T : DomainEvent;

    /// <summary>
    /// Removes all events from the event bus.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    void ClearEvents();

    /// <summary>
    /// Removes an event from the event bus.
    /// </summary>
    /// <param name="domainEvent">The event to be removed.</param>
    public void RemoveEvent<T>(T domainEvent) where T : DomainEvent;

    /// <summary>
    /// Adds a tracked entity to the event bus.
    /// </summary>
    public void AddTrackedEntity<T>(Entity<T> entity) where T : EntityId;

    /// <summary>
    /// Removes a tracked entity from the event bus.
    /// </summary>
    public void RemoveTrackedEntity<T>(Entity<T> entity) where T : EntityId;
}