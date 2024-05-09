namespace ScienceArchive.Application.Dtos.Article;

public record ArticleAuthorDto
{
    public required string UserId { get; set; }
    public required string Name { get; set; }
    public required int Role { get; set; }
}