using ScienceArchive.Core.Domain.Common.Identifiers;

namespace ScienceArchive.Core.Domain.Common;

/// <summary>
/// Represents main entity in aggregate
/// </summary>
/// <typeparam name="TId">Type of ID used in this aggregate root</typeparam>
public abstract class AggregateRoot<TId> : Entity<TId> where TId : EntityId
{
	protected AggregateRoot(TId id) : base(id)
	{
	}
}