using ScienceArchive.Core.Domain.Aggregates.User.ValueObjects;
using ScienceArchive.Core.Domain.Common;

namespace ScienceArchive.Core.Domain.Events;

/// <summary>
/// Represents an event indicating that a user has been registered.
/// </summary>
public sealed record UserRegisteredEvent(
    UserId UserId, 
    string Name, 
    string ConfirmationCode, 
    string Email) : DomainEvent;