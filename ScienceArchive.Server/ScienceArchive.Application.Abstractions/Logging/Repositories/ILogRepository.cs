using ScienceArchive.Application.Abstractions.Logging.Models;

namespace ScienceArchive.Application.Abstractions.Logging.Repositories;

public interface ILogRepository
{
    Task<RequestLog> LogRequest(RequestLog log);
}