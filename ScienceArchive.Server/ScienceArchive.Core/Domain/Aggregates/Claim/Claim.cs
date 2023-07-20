using ScienceArchive.Core.Domain.Aggregates.Claim.ValueObjects;
using ScienceArchive.Core.Domain.Common;

namespace ScienceArchive.Core.Domain.Aggregates.Claim;

/// <summary>
/// Claim entity. Represents permission of
/// doing some actions in the system
/// </summary>
public class Claim : Entity<ClaimId>
{
    public Claim(ClaimId? id = null) : base(id ?? ClaimId.CreateNew())
    {
    }

    /// <summary>
    /// Claim value
    /// </summary>
    public required string Value { get; init; }

    /// <summary>
    /// Claim name
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Claim description
    /// </summary>
    public string? Description { get; set; }
}