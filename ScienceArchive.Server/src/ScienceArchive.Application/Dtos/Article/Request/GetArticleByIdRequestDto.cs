using MediatR;
using ScienceArchive.Application.Dtos.Article.Response;

namespace ScienceArchive.Application.Dtos.Article.Request;

/// <summary>
/// Request contract to get article by its ID
/// </summary>
/// <param name="Id">Identifier of an article</param>
public record GetArticleByIdRequestDto(string Id, string? UserId, List<string> RequiredClaims) : IRequest<GetArticleByIdResponseDto>;