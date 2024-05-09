using ScienceArchive.Application.Abstractions.Logging.Gateways;
using ScienceArchive.Application.Abstractions.Logging.Models;

namespace ScienceArchive.Infrastructure.Connectivity.RabbitMq.Gateways;

internal class RabbitMqLogGateway : ILogGateway
{
    public Task LogRequest(RequestLog log)
    {
        throw new NotImplementedException();
    }
}