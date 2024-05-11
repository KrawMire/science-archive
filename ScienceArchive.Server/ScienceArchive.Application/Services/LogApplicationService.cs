using ScienceArchive.Application.Abstractions.Logging.Gateways;
using ScienceArchive.Application.Dtos.Log.Request;
using ScienceArchive.Application.Dtos.Log.Response;
using ScienceArchive.Application.Interfaces.Services;

namespace ScienceArchive.Application.Services;

internal class LogApplicationService : ILogApplicationService
{
    private readonly ILogGateway _logGateway;
    
    public LogApplicationService(ILogGateway logGateway)
    {
        _logGateway = logGateway;
    }

    public async Task<LogRequestResponseDto> LogRequest(LogRequestRequestDto dto)
    {
        await _logGateway.LogRequest(dto.RequestLog);
        return new LogRequestResponseDto();
    }
}