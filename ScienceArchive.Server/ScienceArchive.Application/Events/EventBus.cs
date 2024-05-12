using MediatR;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Common;

namespace ScienceArchive.Application.Events;

/// <summary>
/// Represents an event bus that handles events.
/// </summary>
internal class EventBus : IEventBus
{
    private readonly IMediator _mediator;
    private readonly ICollection<INotification> _events;
    
    public EventBus(IMediator mediator)
    {
        _mediator = mediator;
        _events = new List<INotification>();
    }

    /// <inheritdoc/>
    public async Task HandleEvents()
    {
        foreach (var domainEvent in _events)
        {
            await _mediator.Publish(domainEvent);
        }

        await ClearEvents();
    }

    /// <inheritdoc/>
    public Task AddEventAsync<T>(EventWrapper<T> domainEvent)
        where T : DomainEvent
    {
        _events.Add(domainEvent);
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task ClearEvents()
    {
        _events.Clear();
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task RemoveEventAsync<T>(EventWrapper<T> domainEvent)
        where T : DomainEvent
    {
        _events.Remove(domainEvent);
        return Task.CompletedTask;
    }
}