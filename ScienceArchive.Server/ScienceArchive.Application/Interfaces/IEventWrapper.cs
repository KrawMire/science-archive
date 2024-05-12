using MediatR;
using ScienceArchive.Core.Domain.Common;

namespace ScienceArchive.Application.Interfaces;

/// <summary>
/// Represents an interface for an event wrapper.
/// </summary>
internal record EventWrapper<TDomainEvent>(TDomainEvent Event) : INotification 
    where TDomainEvent : DomainEvent;