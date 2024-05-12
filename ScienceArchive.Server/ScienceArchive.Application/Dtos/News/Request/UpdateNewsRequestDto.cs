using MediatR;
using ScienceArchive.Application.Dtos.News.Response;

namespace ScienceArchive.Application.Dtos.News.Request;

/// <summary>
/// Request contract to update news
/// </summary>
/// <param name="Id">Identifier of the news</param>
/// <param name="News">New news data to update</param>
public record UpdateNewsRequestDto(string Id, NewsDto News) : IRequest<UpdateNewsResponseDto>;