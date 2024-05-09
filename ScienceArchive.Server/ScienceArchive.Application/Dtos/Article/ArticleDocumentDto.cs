namespace ScienceArchive.Application.Dtos.Article;

public record ArticleDocumentDto
{
    public string? Id { get; set; }
    public required string Name { get; set; }
    public required string Path { get; set; }
}