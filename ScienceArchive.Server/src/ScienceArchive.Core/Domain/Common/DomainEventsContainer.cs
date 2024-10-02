namespace ScienceArchive.Core.Domain.Common;

public abstract class DomainEventsContainer
{
    private readonly List<DomainEvent> _events = [];

    public IReadOnlyCollection<DomainEvent> Events => _events.AsReadOnly();
    
    protected void AddDomainEvent(DomainEvent @event)
    {
        _events.Add(@event);
    }
}