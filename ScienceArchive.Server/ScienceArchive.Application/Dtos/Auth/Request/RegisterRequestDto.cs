using MediatR;
using ScienceArchive.Application.Dtos.Auth.Response;
using ScienceArchive.Application.Dtos.User;

namespace ScienceArchive.Application.Dtos.Auth.Request;

/// <summary>
/// Sign up request contract
/// </summary>
/// <param name="User">New user to sign up</param>
public record RegisterRequestDto(UserDto User, string Password) : IRequest<RegisterResponseDto>;