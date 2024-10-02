using ScienceArchive.Application.Dtos.System.Request;
using ScienceArchive.Application.Dtos.System.Response;

namespace ScienceArchive.Application.Interfaces.Services;

/// <summary>
/// Application system service
/// </summary>
public interface ISystemApplicationService
{
    /// <summary>
    /// Check system status
    /// </summary>
    /// <param name="dto">DTO contract to check system status</param>
    /// <returns>Response DTO</returns>
    Task<CheckSystemStatusResponseDto> CheckSystemStatus(CheckSystemStatusRequestDto dto);
}