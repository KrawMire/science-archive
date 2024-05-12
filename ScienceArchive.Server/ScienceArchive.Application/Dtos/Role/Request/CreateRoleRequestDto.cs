using MediatR;
using ScienceArchive.Application.Dtos.Role.Response;

namespace ScienceArchive.Application.Dtos.Role.Request;

/// <summary>
/// Request contract to create role
/// </summary>
/// <param name="Role">Role to create</param>
public record CreateRoleRequestDto(RoleDto Role) : IRequest<CreateRoleResponseDto>;