using Microsoft.Extensions.DependencyInjection;
using ScienceArchive.Application.Abstractions.Persistence;
using ScienceArchive.Core.Domain.Aggregates.Article;
using ScienceArchive.Core.Domain.Aggregates.Article.Repositories;
using ScienceArchive.Core.Domain.Aggregates.Category;
using ScienceArchive.Core.Domain.Aggregates.Category.Entities;
using ScienceArchive.Core.Domain.Aggregates.Category.Repositories;
using ScienceArchive.Core.Domain.Aggregates.News;
using ScienceArchive.Core.Domain.Aggregates.News.Repositories;
using ScienceArchive.Core.Domain.Aggregates.Notification.Repositories;
using ScienceArchive.Core.Domain.Aggregates.Role;
using ScienceArchive.Core.Domain.Aggregates.Role.Repositories;
using ScienceArchive.Core.Domain.Aggregates.Role.ValueObjects;
using ScienceArchive.Core.Domain.Aggregates.User;
using ScienceArchive.Core.Domain.Aggregates.User.Repositories;
using ScienceArchive.Infrastructure.Interfaces;
using ScienceArchive.Infrastructure.Persistence.Options;
using ScienceArchive.Infrastructure.Persistence.PostgreSql;
using ScienceArchive.Infrastructure.Persistence.PostgreSql.Mappers;
using ScienceArchive.Infrastructure.Persistence.PostgreSql.Models;
using ScienceArchive.Infrastructure.Persistence.PostgreSql.Repositories;

namespace ScienceArchive.Infrastructure.Persistence;

internal static class PersistenceRegistry
{
    /// <summary>
    /// Register all required persistence services
    /// </summary>
    /// <param name="services">Instance of <see cref="IServiceCollection"/></param>
    /// <param name="connectionOptions">Options with connection parameters</param>
    public static IServiceCollection RegisterPersistenceServices(this IServiceCollection services, PersistenceConnectionOptions connectionOptions)
    {
        return services
            .RegisterRepositories()
            .RegisterDbContext()
            .RegisterPersistenceMappers()
            .RegisterConnectionOptions(connectionOptions);
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

    private static IServiceCollection RegisterDbContext(this IServiceCollection services)
    {
        return services
            .AddScoped<PostgresExecutionContext>()
            .AddScoped<PostgresDbContext>()
            .AddScoped<IDbContext>(provider => provider.GetService<PostgresDbContext>()
                                               ?? throw new InvalidOperationException("PostgresDbContext was not found as injectable service"));
    }

    private static IServiceCollection RegisterPersistenceMappers(this IServiceCollection services)
    {
        // Register mappers from entities to models and vice versa
        return services
            .AddTransient<IInfrastructureMapper<Article, ArticleModel>, ArticleMapper>()
            .AddTransient<IInfrastructureMapper<Category, CategoryModel>, CategoryMapper>()
            .AddTransient<IInfrastructureMapper<Subcategory, SubcategoryModel>, SubcategoryMapper>()
            .AddTransient<IInfrastructureMapper<News, NewsModel>, NewsMapper>()
            .AddTransient<IInfrastructureMapper<Role, RoleModel>, RoleMapper>()
            .AddTransient<IInfrastructureMapper<RoleClaim, RoleClaimModel>, RoleClaimMapper>()
            .AddTransient<IInfrastructureMapper<User, UserModel>, UserMapper>();
    }
    
    private static IServiceCollection RegisterConnectionOptions(this IServiceCollection services, PersistenceConnectionOptions connectionOptions)
    {
        return services.AddSingleton(connectionOptions.PostgresConnectionOptions);
    }
}