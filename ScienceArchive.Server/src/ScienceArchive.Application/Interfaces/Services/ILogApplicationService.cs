using ScienceArchive.Application.Dtos.Log.Request;
using ScienceArchive.Application.Dtos.Log.Response;

namespace ScienceArchive.Application.Interfaces.Services;

/// <summary>
/// Represents an interface for logging application service.
/// </summary>
public interface ILogApplicationService
{
    /// <summary>
    /// Logs a request.
    /// </summary>
    /// <param name="dto">The request DTO.</param>
    /// <returns>The response DTO.</returns>
    public Task<LogRequestResponseDto> LogRequest(LogRequestRequestDto dto);
}