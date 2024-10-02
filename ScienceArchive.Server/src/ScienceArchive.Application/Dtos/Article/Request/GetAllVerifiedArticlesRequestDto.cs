using MediatR;
using ScienceArchive.Application.Dtos.Article.Response;

namespace ScienceArchive.Application.Dtos.Article.Request;

/// <summary>
/// Request DTO to get all verified articles
/// </summary>
public record GetAllVerifiedArticlesRequestDto : IRequest<GetAllVerifiedArticlesResponseDto>;