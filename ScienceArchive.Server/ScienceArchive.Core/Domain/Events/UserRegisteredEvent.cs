using ScienceArchive.Core.Domain.Common;

namespace ScienceArchive.Core.Domain.Events;

/// <summary>
/// Represents an event indicating that a user has been registered.
/// </summary>
public class UserRegisteredEvent : DomainEvent
{
    /// <summary>
    /// Represents the email or registered user
    /// </summary>
    public required string Email { get; set; }
}