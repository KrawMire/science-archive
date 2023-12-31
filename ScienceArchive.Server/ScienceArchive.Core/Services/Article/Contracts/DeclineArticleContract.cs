using ScienceArchive.Core.Domain.Aggregates.Article.ValueObjects;

namespace ScienceArchive.Core.Services.ArticleContracts;

/// <summary>
/// Contract to decline article
/// </summary>
/// <param name="ArticleId">Article ID to decline</param>
public record DeclineArticleContract(ArticleId ArticleId);