using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ScienceArchive.Application.Abstractions.Persistence;
using ScienceArchive.Core.Domain.Aggregates.Article.Repositories;
using ScienceArchive.Core.Domain.Aggregates.Category.Repositories;
using ScienceArchive.Core.Domain.Aggregates.News.Repositories;
using ScienceArchive.Core.Domain.Aggregates.Notification.Repositories;
using ScienceArchive.Core.Domain.Aggregates.Role.Repositories;
using ScienceArchive.Core.Domain.Aggregates.User.Repositories;
using ScienceArchive.Infrastructure.Persistence.Options;
using ScienceArchive.Infrastructure.Persistence.PostgreSql;
using ScienceArchive.Infrastructure.Persistence.PostgreSql.Repositories;

namespace ScienceArchive.Infrastructure.Persistence;

internal static class PersistenceRegistry
{
    /// <summary>
    /// Register all required persistence services
    /// </summary>
    /// <param name="services">Instance of <see cref="IServiceCollection"/></param>
    /// <param name="persistenceOptions">Options for persistence</param>
    public static IServiceCollection RegisterPersistenceServices(this IServiceCollection services, PersistenceOptions persistenceOptions)
    {
        return services
            .RegisterRepositories()
            .RegisterDbContext(persistenceOptions)
            .RegisterPersistenceMappers();
    }
    
    private static IServiceCollection RegisterRepositories(this IServiceCollection services)
    {
        return services
            .AddTransient<IArticleRepository, PostgresArticleRepository>()
            .AddTransient<ICategoryRepository, PostgresCategoryRepository>()
            .AddTransient<INewsRepository, PostgresNewsRepository>()
            .AddTransient<INotificationRepository, PostgresNotificationRepository>()
            .AddTransient<IRoleRepository, PostgresRoleRepository>()
            .AddTransient<IUserRepository, PostgresUserRepository>();
    }

    private static IServiceCollection RegisterDbContext(this IServiceCollection services, PersistenceOptions persistenceOptions)
    {
        return services
            .AddDbContext<PostgresDbContext>(options =>
            {
                options.UseNpgsql(persistenceOptions.PostgresConnectionOptions.PostgresConnectionString);
                
            })
            .AddScoped<PostgresDbExecutionContext>()
            .AddScoped<PostgresDbUnitOfWork>()
            .AddScoped<IDbUnitOfWork>(provider => provider.GetService<PostgresDbUnitOfWork>()
                                               ?? throw new InvalidOperationException("PostgresDbUnitOfWork was not found as injectable service"));
    }

    private static IServiceCollection RegisterPersistenceMappers(this IServiceCollection services)
    {
        return services;
    }
}