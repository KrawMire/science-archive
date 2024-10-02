namespace ScienceArchive.Application.Dtos.Article.Response;

/// <summary>
/// DTO response contract of article decline 
/// </summary>
/// <param name="Article">Declined article</param>
public record DeclineArticleResponseDto(ArticleDto Article);