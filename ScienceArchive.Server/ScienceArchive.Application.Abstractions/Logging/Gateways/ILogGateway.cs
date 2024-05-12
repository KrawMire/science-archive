using ScienceArchive.Application.Abstractions.Logging.Models;

namespace ScienceArchive.Application.Abstractions.Logging.Gateways;

/// <summary>
/// Interface for logging request information.
/// </summary>
public interface ILogGateway
{
    /// <summary>
    /// Logs request information.
    /// </summary>
    /// <param name="log">The request information to be logged.</param>
    Task LogRequest(RequestLog log);
}