using ScienceArchive.Application.Abstractions.Persistence;
using ScienceArchive.Application.Dtos.Log.Request;
using ScienceArchive.Application.Dtos.Log.Response;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Application.Interfaces.Services;
using ScienceArchive.Application.Services.Common;

namespace ScienceArchive.Application.Services;

internal class LogApplicationService : BaseApplicationService, ILogApplicationService
{
    public LogApplicationService(IServiceProvider serviceProvider, IDbContext dbContext, IEventBus eventBus) 
        : base(serviceProvider, dbContext, eventBus) { }

    public Task<LogRequestResponseDto> LogRequest(LogRequestRequestDto dto)
    {
        return ExecuteUseCase<LogRequestRequestDto, LogRequestResponseDto>(dto);
    }
}