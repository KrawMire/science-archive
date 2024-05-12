using MediatR;
using ScienceArchive.Application.Dtos.News.Response;

namespace ScienceArchive.Application.Dtos.News.Request;

/// <summary>
/// Request contract to get all news
/// </summary>
public record GetAllNewsRequestDto : IRequest<GetAllNewsResponseDto>;