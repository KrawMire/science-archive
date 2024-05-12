using ScienceArchive.Application.Abstractions.Logging.Gateways;
using ScienceArchive.Application.Dtos.Log.Request;
using ScienceArchive.Application.Dtos.Log.Response;
using ScienceArchive.Application.Interfaces;

namespace ScienceArchive.Application.UseCases.LogUseCases;

public class LogRequestUseCase : IUseCase<LogRequestRequestDto, LogRequestResponseDto>
{
    private readonly ILogGateway _logGateway;

    public LogRequestUseCase(ILogGateway logGateway)
    {
        _logGateway = logGateway;
    }

    public async Task<LogRequestResponseDto> Handle(LogRequestRequestDto request, CancellationToken cancellationToken)
    {
        await _logGateway.LogRequest(request.RequestLog);
        return new LogRequestResponseDto();
    }
}