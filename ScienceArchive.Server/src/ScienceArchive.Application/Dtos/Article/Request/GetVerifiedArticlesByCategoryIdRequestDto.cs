using MediatR;
using ScienceArchive.Application.Dtos.Article.Response;

namespace ScienceArchive.Application.Dtos.Article.Request;

/// <summary>
/// DTO contract to get articles by category ID
/// </summary>
/// <param name="CategoryId">Category ID</param>
public record GetVerifiedArticlesByCategoryIdRequestDto(string CategoryId) : IRequest<GetVerifiedArticlesByCategoryIdResponseDto>;