using ScienceArchive.Core.Domain.Aggregates.Article.Enums;
using ScienceArchive.Core.Domain.Aggregates.Article.ValueObjects;
using ScienceArchive.Core.Domain.Common;

namespace ScienceArchive.Core.Domain.Events;

/// <summary>
/// Event that is triggered when the status of an article is changed.
/// </summary>
public class ArticleStatusChangedEvent : DomainEvent
{
    /// <summary>
    /// Identifier of an article.
    /// </summary>
    public required ArticleId ArticleId { get; set; }

    /// <summary>
    /// Represents the status of an article.
    /// </summary>
    public required ArticleStatus Status { get; set; }

    /// <summary>
    /// The message associated with an article status change event.
    /// </summary>
    public string? Message { get; set; } 
}