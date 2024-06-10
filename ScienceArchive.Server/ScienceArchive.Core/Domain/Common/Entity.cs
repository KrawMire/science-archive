using ScienceArchive.Core.Domain.Common.Identifiers;

namespace ScienceArchive.Core.Domain.Common;

/// <summary>
/// Represents object in system which
/// is determined by its identifier
/// </summary>
/// <typeparam name="TId">Type of ID used in this entity</typeparam>
public abstract class Entity<TId> : DomainEventsContainer 
    where TId : EntityId
{
    protected Entity(TId id)
    {
        Id = id;
    }

    /// <summary>
    /// Global identifier of the entity
    /// </summary>
    public TId Id { get; private set; }
}