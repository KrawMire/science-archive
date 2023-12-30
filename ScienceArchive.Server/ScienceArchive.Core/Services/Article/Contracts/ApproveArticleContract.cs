using ScienceArchive.Core.Domain.Aggregates.Article.ValueObjects;

namespace ScienceArchive.Core.Services.ArticleContracts;

/// <summary>
/// Contract to approve article
/// </summary>
/// <param name="ArticleId">Article ID to approve</param>
public record ApproveArticleContract(ArticleId ArticleId);