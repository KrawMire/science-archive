using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Common;

namespace ScienceArchive.Application.Events;

/// <summary>
/// Represents an event bus that handles events.
/// </summary>
internal class EventBus : IEventBus
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ICollection<DomainEvent> _events;
    
    public EventBus(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _events = new List<DomainEvent>();
    }

    /// <inheritdoc/>
    public Task HandleEvents()
    {
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task AddEventAsync(DomainEvent domainEvent)
    {
        _events.Add(domainEvent);
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task RemoveEventAsync(DomainEvent domainEvent)
    {
        _events.Remove(domainEvent);
        return Task.CompletedTask;
    }
}