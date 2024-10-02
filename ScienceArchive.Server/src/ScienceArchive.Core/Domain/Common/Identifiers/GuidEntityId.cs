namespace ScienceArchive.Core.Domain.Common.Identifiers;

public class GuidEntityId : EntityId
{
    protected GuidEntityId(Guid value)
    {
        Value = value;
    }
    
    /// <summary>
    /// The value of the identifier
    /// </summary>
    public Guid Value { get; protected set; }
}