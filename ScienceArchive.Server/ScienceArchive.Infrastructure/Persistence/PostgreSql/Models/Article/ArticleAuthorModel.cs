namespace ScienceArchive.Infrastructure.Persistence.PostgreSql.Models;

public record ArticleAuthorModel
{
    public required Guid AuthorId { get; set; }
    public required string AuthorName { get; set; }
    public required int Role { get; set; }
}