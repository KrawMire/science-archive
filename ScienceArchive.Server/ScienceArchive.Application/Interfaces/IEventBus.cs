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
    public Task AddEventAsync<T>(EventWrapper<T> domainEvent) where T : DomainEvent;

    /// <summary>
    /// Removes all events from the event bus.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task ClearEvents();

    /// <summary>
    /// Removes an event from the event bus.
    /// </summary>
    /// <param name="domainEvent">The event to be removed.</param>
    public Task RemoveEventAsync<T>(EventWrapper<T> domainEvent) where T : DomainEvent;
}