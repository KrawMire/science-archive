using ScienceArchive.Core.Domain.Aggregates.Article.Enums;
using ScienceArchive.Core.Domain.Aggregates.Article.ValueObjects;
using ScienceArchive.Core.Domain.Aggregates.Category.ValueObjects;
using ScienceArchive.Core.Domain.Aggregates.User.ValueObjects;
using ScienceArchive.Core.Domain.Common;

namespace ScienceArchive.Core.Domain.Aggregates.Article;

/// <summary>
/// Article entity
/// </summary>
public class Article : Entity<ArticleId>
{
    private ArticleStatus _status;
    
    public Article(ArticleId? id = null) : base(id ?? ArticleId.CreateNew())
    {
    }
    
    /// <summary>
    /// Category of an article
    /// </summary>
    public required CategoryId CategoryId { get; set; }
    
    /// <summary>
    /// Article's title
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    /// Author of an article
    /// </summary>
    public required List<UserId> AuthorsIds { get; set; }

    /// <summary>
    /// Current status of an article
    /// </summary>
    public required ArticleStatus Status
    {
        get => _status;
        init => _status = value;
    }

    /// <summary>
    /// Date when article was created
    /// </summary>
    public required DateTime CreationDate { get; set; }

    /// <summary>
    /// Linked article document
    /// </summary>
    public required List<ArticleDocument> Documents { get; set; }
    
    /// <summary>
    /// Article description
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Approve article
    /// </summary>
    public void Approve()
    {
        _status = ArticleStatus.Verified;
    }

    /// <summary>
    /// Decline article
    /// </summary>
    public void Decline()
    {
        _status = ArticleStatus.Declined;
    }

    /// <summary>
    /// Set article status as "to verify"
    /// </summary>
    public void SetToVerify()
    {
        _status = ArticleStatus.ToVerify;
    }
}