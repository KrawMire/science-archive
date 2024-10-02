using ScienceArchive.Core.Domain.Aggregates.Article.Enums;
using ScienceArchive.Core.Domain.Aggregates.Article.ValueObjects;
using ScienceArchive.Core.Domain.Common;

namespace ScienceArchive.Core.Domain.Events;

/// <summary>
/// Event that is triggered when the status of an article is changed.
/// </summary>
public sealed record ArticleStatusChangedEvent(
    ArticleId ArticleId, 
    ArticleStatus Status, 
    string? Message) : DomainEvent;