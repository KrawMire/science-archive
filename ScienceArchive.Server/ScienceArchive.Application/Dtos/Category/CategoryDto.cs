namespace ScienceArchive.Application.Dtos.Category;

public record CategoryDto
{
	public required string Id { get; set; }
	public required string Name { get; set; }
	public string? Description { get; set; }
	public List<CategoryDto>? Subcategories { get; set; }
}