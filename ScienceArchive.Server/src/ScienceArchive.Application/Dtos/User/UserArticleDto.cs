namespace ScienceArchive.Application.Dtos.User;

public record UserArticleDto
{
    public required string ArticleId { get; set; }
    public required string Title { get; set; }
}