using Microsoft.Extensions.DependencyInjection;
using ScienceArchive.Shared.Abstractions.Logging.Gateways;
using ScienceArchive.Core.Gateways;
using ScienceArchive.Infrastructure.Connectivity.Options;
using ScienceArchive.Infrastructure.Connectivity.RabbitMq.Gateways;
using ScienceArchive.Infrastructure.Connectivity.RabbitMq.Options;

namespace ScienceArchive.Infrastructure.Connectivity;

internal static class ConnectivityRegistry
{
	public static IServiceCollection RegisterConnectivityServices(this IServiceCollection services, ConnectivityOptions connectivityOptions)
	{
		return services
			.RegisterRabbitMqOptions(connectivityOptions.RabbitMqConnectionOptions)
			.RegisterGateways();
	}

	private static IServiceCollection RegisterRabbitMqOptions(this IServiceCollection services, RabbitMqConnectionOptions connectionOptions)
	{
		return services
			.AddSingleton(connectionOptions);
	}

	private static IServiceCollection RegisterGateways(this IServiceCollection services)
	{
		return services
			.AddSingleton<ILogGateway, RabbitMqLogGateway>()
			.AddSingleton<INotificationGateway, RabbitMqNotificationGateway>();
	}
}