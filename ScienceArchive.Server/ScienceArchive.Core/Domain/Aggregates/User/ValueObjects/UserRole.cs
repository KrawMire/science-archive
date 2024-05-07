using ScienceArchive.Core.Domain.Aggregates.Role.ValueObjects;
using ScienceArchive.Core.Domain.Common;

namespace ScienceArchive.Core.Domain.Aggregates.User.ValueObjects;

public class UserRole : ValueObject
{
    /// <summary>
    /// ID of role
    /// </summary>
    public required RoleId RoleId { get; init; }
}