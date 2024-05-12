using MediatR;
using ScienceArchive.Application.Dtos.Article.Response;

namespace ScienceArchive.Application.Dtos.Article.Request;

/// <summary>
/// Request contract to get all articles
/// </summary>
public record GetAllArticlesRequestDto : IRequest<GetAllArticlesResponseDto>;