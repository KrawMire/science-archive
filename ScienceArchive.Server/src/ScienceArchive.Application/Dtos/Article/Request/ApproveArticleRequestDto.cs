using MediatR;
using ScienceArchive.Application.Dtos.Article.Response;

namespace ScienceArchive.Application.Dtos.Article.Request;

/// <summary>
/// DTO contract to approve article
/// </summary>
/// <param name="ArticleId">Article ID to approve</param>
public record ApproveArticleRequestDto(string ArticleId) : IRequest<ApproveArticleResponseDto>;