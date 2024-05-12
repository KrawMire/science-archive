using ScienceArchive.Infrastructure.Connectivity.RabbitMq.Options;

namespace ScienceArchive.Infrastructure.Connectivity.Options;

public class ConnectivityOptions
{
    public required RabbitMqConnectionOptions RabbitMqConnectionOptions { get; set; }
}