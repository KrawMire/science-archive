namespace ScienceArchive.Infrastructure.Persistence.PostgreSql.Models;

public record UserArticleModel
{
    public required Guid ArticleId { get; set; }
    public required string Title { get; set; }
}