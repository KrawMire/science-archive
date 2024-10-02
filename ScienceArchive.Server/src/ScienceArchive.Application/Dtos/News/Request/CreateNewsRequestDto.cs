using MediatR;
using ScienceArchive.Application.Dtos.News.Response;

namespace ScienceArchive.Application.Dtos.News.Request;

/// <summary>
/// Request contract to create news
/// </summary>
/// <param name="News">News to create</param>
public record CreateNewsRequestDto(NewsDto News) : IRequest<CreateNewsResponseDto>;