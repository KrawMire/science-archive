using Microsoft.Extensions.DependencyInjection;
using ScienceArchive.Core.Gateways;
using ScienceArchive.Infrastructure.Connectivity.RabbitMq;
using ScienceArchive.Infrastructure.Connectivity.RabbitMq.Gateways;

namespace ScienceArchive.Infrastructure.Connectivity;

internal static class ConnectivityRegistry
{
	public static IServiceCollection RegisterConnectivityServices(this IServiceCollection services)
	{
		return services.AddSingleton<INotificationGateway, RabbitMqNotificationGateway>();
	}
}