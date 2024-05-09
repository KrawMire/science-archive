using ScienceArchive.Core.Domain.Common;

namespace ScienceArchive.Application.Interfaces;

/// <summary>
/// Defines the interface for handling events.
/// </summary>
internal interface IEventBus
{
    /// <summary>
    /// Handles events in the event bus.
    /// </summary>
    Task HandleEvents();

    /// <summary>
    /// Adds an event to the event bus.
    /// </summary>
    /// <param name="domainEvent">The event to be added.</param>
    Task AddEventAsync(DomainEvent domainEvent);

    /// <summary>
    /// Removes an event from the event bus.
    /// </summary>
    /// <param name="domainEvent">The event to be removed.</param>
    Task RemoveEventAsync(DomainEvent domainEvent);
}