using ScienceArchive.Core.Domain.Aggregates.User.ValueObjects;
using ScienceArchive.Core.Domain.Common;

namespace ScienceArchive.Core.Domain.Events;

/// <summary>
/// Represents an event indicating that a user has been registered.
/// </summary>
public class UserRegisteredEvent : DomainEvent
{
    /// <summary>
    /// Represents the unique identifier of a user.
    /// </summary>
    public required UserId UserId { get; set; }

    /// <summary>
    /// Represents the name of a user.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Represents the confirmation code for user registration.
    /// </summary>
    public required string ConfirmationCode { get; set; }
    
    /// <summary>
    /// Represents the email or registered user
    /// </summary>
    public required string Email { get; set; }
}