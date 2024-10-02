using MediatR;
using ScienceArchive.Application.Dtos.User.Response;

namespace ScienceArchive.Application.Dtos.User.Request;

/// <summary>
/// Request contract to get all users
/// </summary>
public record GetAllUsersRequestDto : IRequest<GetAllUsersResponseDto>;