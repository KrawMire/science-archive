using ScienceArchive.Application.Dtos.System.Request;
using ScienceArchive.Application.Dtos.System.Response;
using ScienceArchive.Application.Interfaces.Services;
using ScienceArchive.Application.Services.Common;

namespace ScienceArchive.Application.Services;

internal class SystemApplicationService : BaseApplicationService, ISystemApplicationService
{
    public SystemApplicationService(IServiceProvider serviceProvider) : base(serviceProvider) { }

    /// <inheritdoc/>
    public Task<CheckSystemStatusResponseDto> CheckSystemStatus(CheckSystemStatusRequestDto dto)
    {
        return ExecuteUseCase<CheckSystemStatusRequestDto, CheckSystemStatusResponseDto>(dto);
    }
}