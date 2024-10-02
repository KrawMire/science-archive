using MediatR;
using ScienceArchive.Application.Dtos.Article.Response;

namespace ScienceArchive.Application.Dtos.Article.Request;

/// <summary>
/// DTO contract to get verified articles by author ID
/// </summary>
/// <param name="AuthorId">Author ID</param>
public record GetVerifiedArticlesByAuthorIdRequestDto(string AuthorId) : IRequest<GetVerifiedArticlesByAuthorIdResponseDto>;