using ScienceArchive.Core.Domain.Aggregates.Article.Entities;
using ScienceArchive.Core.Domain.Aggregates.Article.Enums;
using ScienceArchive.Core.Domain.Aggregates.Article.ValueObjects;
using ScienceArchive.Core.Domain.Common;
using ScienceArchive.Core.Domain.Events;
using ScienceArchive.Core.Exceptions;

namespace ScienceArchive.Core.Domain.Aggregates.Article;

/// <summary>
/// Article entity
/// </summary>
public class Article : AggregateRoot<ArticleId>
{
    private string _title;
    private string? _description;
    private ArticleStatus _status;
    
    internal Article(ArticleId? id) : base(id ?? ArticleId.CreateNew())
    {
        _title = string.Empty;
        _status = ArticleStatus.ToVerify;
    }
    
    /// <summary>
    /// Category of an article
    /// </summary>
    public required ArticleCategory Category { get; init; }

    /// <summary>
    /// Article's title
    /// </summary>
    public required string Title
    {
        get => _title;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidFieldValueException(nameof(Title));
            }

            _title = value.Trim();
        }
    }

    /// <summary>
    /// Authors of an article
    /// </summary>
    public required List<ArticleAuthor> Authors { get; init; }

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
    public required DateTime CreationDate { get; init; }

    /// <summary>
    /// Linked article document
    /// </summary>
    public required List<ArticleDocument> Documents { get; init; }

    /// <summary>
    /// Article description
    /// </summary>
    public string? Description
    {
        get => _description;
        set => _description = value?.Trim();
    }

    /// <summary>
    /// Approve article
    /// </summary>
    public void Approve()
    {
        _status = ArticleStatus.Verified;
        
        AddDomainEvent(new ArticleStatusChangedEvent(Id, Status, Message: null));
    }

    /// <summary>
    /// Decline article
    /// </summary>
    public void Decline()
    {
        _status = ArticleStatus.Declined;

        AddDomainEvent(new ArticleStatusChangedEvent(Id, Status, Message: null));
    }

    /// <summary>
    /// Set article status as "to verify"
    /// </summary>
    public void SetToVerify()
    {
        _status = ArticleStatus.ToVerify;
        
        AddDomainEvent(new ArticleStatusChangedEvent(Id, Status, Message: null));
    }
}