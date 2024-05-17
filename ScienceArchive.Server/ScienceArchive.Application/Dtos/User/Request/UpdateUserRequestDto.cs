using MediatR;
using ScienceArchive.Application.Dtos.User.Response;

namespace ScienceArchive.Application.Dtos.User.Request;

/// <summary>
/// Request contract to update user
/// </summary>
/// <param name="User">User data</param>
public record UpdateUserRequestDto(string Id, UserDto User, string InitiatorUserId) : IRequest<UpdateUserResponseDto>;