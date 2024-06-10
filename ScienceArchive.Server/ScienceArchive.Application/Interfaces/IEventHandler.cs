using MediatR;
using ScienceArchive.Core.Domain.Common;

namespace ScienceArchive.Application.Interfaces;

/// <summary>
/// Represents an interface for event handlers.
/// </summary>
/// <typeparam name="TEvent">The type of domain event.</typeparam>
internal interface IEventHandler<in TEvent> : INotificationHandler<TEvent>
    where TEvent : DomainEvent;