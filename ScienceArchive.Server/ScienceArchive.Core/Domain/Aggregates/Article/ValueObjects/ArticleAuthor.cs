using ScienceArchive.Core.Domain.Aggregates.Article.Enums;
using ScienceArchive.Core.Domain.Aggregates.User.ValueObjects;
using ScienceArchive.Core.Domain.Common;

namespace ScienceArchive.Core.Domain.Aggregates.Article.ValueObjects;

public class ArticleAuthor : ValueObject
{
    /// <summary>
    /// ID of user which is an author of article
    /// </summary>
    public required UserId UserId { get; set; }
    
    /// <summary>
    /// Name of user
    /// </summary>
    public required string Name { get; set; }
    
    /// <summary>
    /// Role of author in this article
    /// </summary>
    public required ArticleAuthorRole Role { get; set; }
}