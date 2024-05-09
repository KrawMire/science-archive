using Microsoft.Extensions.DependencyInjection;
using ScienceArchive.Application.Abstractions.Logging.Gateways;
using ScienceArchive.Core.Gateways;
using ScienceArchive.Infrastructure.Connectivity.RabbitMq.Gateways;

namespace ScienceArchive.Infrastructure.Connectivity;

internal static class ConnectivityRegistry
{
	public static IServiceCollection RegisterConnectivityServices(this IServiceCollection services)
	{
		return services
			.AddSingleton<ILogGateway, RabbitMqLogGateway>()
			.AddSingleton<INotificationGateway, RabbitMqNotificationGateway>();
	}
}