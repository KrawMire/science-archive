namespace ScienceArchive.Application.Dtos.Article.Request;

/// <summary>
/// DTO contract to approve article
/// </summary>
/// <param name="ArticleId">Article ID to approve</param>
public record ApproveArticleRequestDto(string ArticleId);