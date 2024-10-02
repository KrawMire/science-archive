namespace ScienceArchive.Application.Dtos.Article.Response;

/// <summary>
/// Response contract of article approve
/// </summary>
/// <param name="Article">Approved article</param>
public record ApproveArticleResponseDto(ArticleDto Article);