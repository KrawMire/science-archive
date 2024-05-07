using ScienceArchive.Core.Domain.Aggregates.Article.ValueObjects;

namespace ScienceArchive.Core.Domain.Aggregates.User.ValueObjects;

public class UserArticle
{
    public required ArticleId ArticleId { get; set; }
}