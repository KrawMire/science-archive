using ScienceArchive.Application.Dtos.Role.Request;
using ScienceArchive.Application.Dtos.Role.Response;
using ScienceArchive.Application.Interfaces.Services;
using ScienceArchive.Application.Services.Common;

namespace ScienceArchive.Application.Services;

internal class RoleApplicationService : BaseApplicationService, IRoleApplicationService
{
    public RoleApplicationService(IServiceProvider serviceProvider) : base(serviceProvider) { }
    
    /// <inheritdoc/>
    public Task<GetAllRolesResponseDto> GetAllRoles(GetAllRolesRequestDto dto)
    {
        return ExecuteUseCase<GetAllRolesRequestDto, GetAllRolesResponseDto>(dto);
    }
}