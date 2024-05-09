using ScienceArchive.Core.Domain.Common;

namespace ScienceArchive.Application.Interfaces;

/// <summary>
/// Represents an event handler for domain events.
/// </summary>
/// <typeparam name="T">The type of domain event.</typeparam>
internal interface IEventHandler<in T> where T : DomainEvent
{
    /// <summary>
    /// Handles a domain event.
    /// </summary>
    /// <param name="domainEvent">The domain event to handle.</param>
    Task Handle(T domainEvent);
}