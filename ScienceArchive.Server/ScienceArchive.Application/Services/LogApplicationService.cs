using MediatR;
using ScienceArchive.Application.Abstractions.Persistence;
using ScienceArchive.Application.Dtos.Log.Request;
using ScienceArchive.Application.Dtos.Log.Response;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Application.Interfaces.Services;
using ScienceArchive.Application.Services.Common;

namespace ScienceArchive.Application.Services;

internal class LogApplicationService : BaseApplicationService, ILogApplicationService
{
    public LogApplicationService(IMediator mediator, IDbUnitOfWork dbUnitOfWork, IEventBus eventBus) 
        : base(mediator, dbUnitOfWork, eventBus) { }

    public Task<LogRequestResponseDto> LogRequest(LogRequestRequestDto dto)
    {
        return ExecuteUseCase<LogRequestRequestDto, LogRequestResponseDto>(dto);
    }
}