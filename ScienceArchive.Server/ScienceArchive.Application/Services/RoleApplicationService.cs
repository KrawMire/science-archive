using ScienceArchive.Application.Dtos.Role.Request;
using ScienceArchive.Application.Dtos.Role.Response;
using ScienceArchive.Application.Interfaces.Services;
using ScienceArchive.Application.Services.Common;

namespace ScienceArchive.Application.Services;

/// <summary>
/// Represents a service that handles role-related functionalities in the application.
/// </summary>
internal class RoleApplicationService : BaseApplicationService, IRoleApplicationService
{
    public RoleApplicationService(IServiceProvider serviceProvider) : base(serviceProvider) { }
    
    /// <inheritdoc/>
    public Task<GetAllRolesResponseDto> GetAllRoles(GetAllRolesRequestDto dto)
    {
        return ExecuteUseCase<GetAllRolesRequestDto, GetAllRolesResponseDto>(dto);
    }
}