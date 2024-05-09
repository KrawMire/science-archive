using ScienceArchive.Core.Domain.Aggregates.Article.ValueObjects;

namespace ScienceArchive.Core.Domain.Aggregates.User.ValueObjects;

/// <summary>
/// Represents an article associated with a user.
/// </summary>
public class UserArticle
{
    /// <summary>
    /// Represents the identifier of an article.
    /// </summary>
    public required ArticleId ArticleId { get; set; }

    /// <summary>
    /// Represents the title of an article associated with a user.
    /// </summary>
    public required string Title { get; set; }
}