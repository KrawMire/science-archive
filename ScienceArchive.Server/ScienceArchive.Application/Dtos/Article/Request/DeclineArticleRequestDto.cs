namespace ScienceArchive.Application.Dtos.Article.Request;

/// <summary>
/// DTO contract to decline article
/// </summary>
/// <param name="ArticleId">Article ID to decline</param>
public record DeclineArticleRequestDto(string ArticleId);