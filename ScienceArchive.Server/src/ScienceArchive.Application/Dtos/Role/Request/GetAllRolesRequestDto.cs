using MediatR;
using ScienceArchive.Application.Dtos.Role.Response;

namespace ScienceArchive.Application.Dtos.Role.Request;

/// <summary>
/// Request contract to get all roles
/// </summary>
public record GetAllRolesRequestDto : IRequest<GetAllRolesResponseDto>;