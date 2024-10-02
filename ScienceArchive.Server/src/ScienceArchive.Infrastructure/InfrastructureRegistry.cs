using Microsoft.Extensions.DependencyInjection;
using ScienceArchive.Shared.Abstractions.Encryption;
using ScienceArchive.Core.Domain.Common;
using ScienceArchive.Core.Domain.Services;
using ScienceArchive.Infrastructure.Connectivity;
using ScienceArchive.Infrastructure.Connectivity.Options;
using ScienceArchive.Infrastructure.DomainServices;
using ScienceArchive.Infrastructure.Persistence;
using ScienceArchive.Infrastructure.Persistence.Options;
using ScienceArchive.Infrastructure.Services;
using ScienceArchive.Shared.Abstractions.Templates;

namespace ScienceArchive.Infrastructure;

public static class InfrastructureRegistry
{
    public static IServiceCollection RegisterInfrastructureServices(
        this IServiceCollection services, 
        PersistenceOptions persistenceOptions,
        ConnectivityOptions connectivityOptions)
    {
        return services
            .RegisterPersistenceServices(persistenceOptions)
            .RegisterConnectivityServices(connectivityOptions)
            .RegisterDomainServices()
            .RegisterApplicationServices();
    }

    private static IServiceCollection RegisterDomainServices(this IServiceCollection services)
    {
        return services
            .AddSingleton<IConfirmationService, ConfirmationService>()
            .AddTransient<IAuthService, AuthService>()
            .AddScoped<IDomainEventBus, DomainEventBus>();
    }
    
    private static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
    {
        return services
            .AddTransient<ITemplateService, TemplateService>()
            .AddTransient<IEncryptionService, EncryptionService>();
    }
}