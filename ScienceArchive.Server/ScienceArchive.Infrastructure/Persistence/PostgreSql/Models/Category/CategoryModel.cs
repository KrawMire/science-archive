namespace ScienceArchive.Infrastructure.Persistence.PostgreSql.Models;

internal class CategoryModel
{
	public required Guid Id { get; set; }
	public required string Name { get; set; }
	public string? Description { get; set; }
	public required List<SubcategoryModel> Subcategories { get; set; }
}