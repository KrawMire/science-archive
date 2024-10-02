using ScienceArchive.Core.Domain.Aggregates.Category.Entities;
using ScienceArchive.Core.Domain.Aggregates.Category.ValueObjects;
using ScienceArchive.Core.Domain.Common;

namespace ScienceArchive.Core.Domain.Aggregates.Category.Repositories;

/// <summary>
/// Contains methods for working with
/// category entity through external storages
/// </summary>
public interface ICategoryRepository : ICrudRepository<CategoryId, Category>
{
	/// <summary>
	/// Get subcategory by ID
	/// </summary>
	/// <param name="subcategoryId">Subcategory ID</param>
	/// <returns>Found subcategory or null</returns>
	Task<Subcategory?> GetSubcategoryById(SubcategoryId subcategoryId);
}