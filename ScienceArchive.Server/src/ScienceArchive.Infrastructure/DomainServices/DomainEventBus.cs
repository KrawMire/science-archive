using MediatR;
using ScienceArchive.Core.Domain.Common;
using ScienceArchive.Core.Domain.Common.Identifiers;

namespace ScienceArchive.Infrastructure.DomainServices;

/// <summary>
/// Represents an event bus that handles events.
/// </summary>
internal class DomainEventBus : IDomainEventBus
{
    private readonly IMediator _mediator;
    private readonly ICollection<INotification> _events;
    private readonly ICollection<DomainEventsContainer> _trackedEntities;
    
    public DomainEventBus(IMediator mediator)
    {
        _mediator = mediator;
        _events = new List<INotification>();
        _trackedEntities = new List<DomainEventsContainer>();
    }

    /// <inheritdoc/>
    public async Task HandleEvents()
    {
        DispatchTrackedEntitiesEvents();
        
        foreach (var domainEvent in _events)
        {
            await _mediator.Publish(domainEvent);
        }

        ClearEvents();
    }

    /// <inheritdoc/>
    public void AddEvent<T>(T domainEvent)
        where T : DomainEvent
    {
        _events.Add(domainEvent);
    }

    /// <inheritdoc/>
    public void AddEvents<T>(IEnumerable<T> domainEvents) where T : DomainEvent
    {
        foreach (var domainEvent in domainEvents)
        {
            AddEvent(domainEvent);
        }
    }

    /// <inheritdoc/>
    public void ClearEvents()
    {
        _events.Clear();
    }

    /// <inheritdoc/>
    public void RemoveEvent<T>(T domainEvent)
        where T : DomainEvent
    {
        _events.Remove(domainEvent);
    }

    /// <inheritdoc/>
    public void AddTrackedEntity<T>(Entity<T> entity) where T : EntityId
    {
        _trackedEntities.Add(entity);
    }

    /// <inheritdoc/>
    public void RemoveTrackedEntity<T>(Entity<T> entity) where T : EntityId
    {
        _trackedEntities.Remove(entity);
    }

    private void DispatchTrackedEntitiesEvents()
    {
        foreach (var trackedEntity in _trackedEntities)
        {
            if (!trackedEntity.Events.Any())
            {
                continue;
            }
            
            foreach (var @event in trackedEntity.Events)
            {
                _events.Add(@event);
            }
        }
    }
}