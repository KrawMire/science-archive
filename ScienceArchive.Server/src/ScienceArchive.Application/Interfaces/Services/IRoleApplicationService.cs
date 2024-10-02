using ScienceArchive.Application.Dtos.Role.Request;
using ScienceArchive.Application.Dtos.Role.Response;

namespace ScienceArchive.Application.Interfaces.Services;

/// <summary>
/// Application role service
/// </summary>
public interface IRoleApplicationService
{
    /// <summary>
    /// Get all roles
    /// </summary>
    /// <param name="dto">DTO contract to get all roles</param>
    /// <returns>Response DTO</returns>
    Task<GetAllRolesResponseDto> GetAllRoles(GetAllRolesRequestDto dto);
}