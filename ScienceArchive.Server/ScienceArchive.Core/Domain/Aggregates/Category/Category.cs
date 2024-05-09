using ScienceArchive.Core.Domain.Aggregates.Category.Entities;
using ScienceArchive.Core.Domain.Aggregates.Category.ValueObjects;
using ScienceArchive.Core.Domain.Common;

namespace ScienceArchive.Core.Domain.Aggregates.Category;

/// <summary>
/// Represents category of articles
/// </summary>
public class Category : AggregateRoot<CategoryId>
{
	public Category(CategoryId? id) : base(id ?? CategoryId.CreateNew())
	{
	}
	
	/// <summary>
	/// Name of category
	/// </summary>
	public required string Name { get; init; }
	
	/// <summary>
	/// Subcategories of a category
	/// </summary>
	public required List<Subcategory> Subcategories { get; init; }
	
	/// <summary>
	/// Description of category
	/// </summary>
	public string? Description { get; init; }
}