using ScienceArchive.Core.Domain.Aggregates.Category.ValueObjects;
using ScienceArchive.Core.Domain.Common;

namespace ScienceArchive.Core.Domain.Aggregates.Category.Entities;

public class Subcategory : Entity<SubcategoryId>
{
    protected Subcategory(SubcategoryId id) : base(id) { }
    
    /// <summary>
    /// Name of subcategory
    /// </summary>
    public required string Name { get; init; }
    
    /// <summary>
    /// Description of subcategory
    /// </summary>
    public string? Description { get; init; }
}