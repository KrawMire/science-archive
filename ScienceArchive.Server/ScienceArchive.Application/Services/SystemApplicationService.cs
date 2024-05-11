using ScienceArchive.Application.Abstractions.Persistence;
using ScienceArchive.Application.Dtos.System.Request;
using ScienceArchive.Application.Dtos.System.Response;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Application.Interfaces.Services;
using ScienceArchive.Application.Services.Common;

namespace ScienceArchive.Application.Services;

/// <summary>
/// Represents the system application service.
/// </summary>
/// <remarks>
/// This service is responsible for performing system-related operations and providing system information.
/// </remarks>
internal class SystemApplicationService : BaseApplicationService, ISystemApplicationService
{
    public SystemApplicationService(IServiceProvider serviceProvider, IDbUnitOfWork dbUnitOfWork, IEventBus eventBus) 
        : base(serviceProvider, dbUnitOfWork, eventBus) { }

    /// <inheritdoc/>
    public Task<CheckSystemStatusResponseDto> CheckSystemStatus(CheckSystemStatusRequestDto dto)
    {
        return ExecuteUseCase<CheckSystemStatusRequestDto, CheckSystemStatusResponseDto>(dto);
    }
}