namespace ScienceArchive.Infrastructure.Persistence.PostgreSql.Models;

internal class SubcategoryModel
{
	public required Guid Id { get; set; }
	public required string Name { get; set; }
	public string? Description { get; set; }
}