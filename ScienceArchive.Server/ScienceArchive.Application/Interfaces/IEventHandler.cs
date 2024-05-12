using MediatR;
using ScienceArchive.Core.Domain.Common;

namespace ScienceArchive.Application.Interfaces;

/// <summary>
/// Represents an interface for event handlers.
/// </summary>
/// <typeparam name="TWrapper">The type of event wrapper.</typeparam>
/// <typeparam name="TEvent">The type of domain event.</typeparam>
internal interface IEventHandler<in TWrapper, TEvent> : INotificationHandler<TWrapper>
    where TWrapper : EventWrapper<TEvent>
    where TEvent : DomainEvent;