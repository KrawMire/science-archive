using ScienceArchive.Core.Domain.Common.Identifiers;

namespace ScienceArchive.Core.Domain.Aggregates.Claim.ValueObjects;

public sealed class ClaimId : GuidEntityId<ClaimId>
{
	private ClaimId(Guid value) : base(value)
	{
	}
}