using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using ScienceArchive.Application.Dtos.Article;
using ScienceArchive.Application.Dtos.Category;
using ScienceArchive.Application.Dtos.News;
using ScienceArchive.Application.Dtos.Role;
using ScienceArchive.Application.Dtos.User;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Application.Interfaces.Services;
using ScienceArchive.Application.Mappers;
using ScienceArchive.Application.Services;
using ScienceArchive.Core.Domain.Aggregates.Article;
using ScienceArchive.Core.Domain.Aggregates.Category;
using ScienceArchive.Core.Domain.Aggregates.Category.Entities;
using ScienceArchive.Core.Domain.Aggregates.News;
using ScienceArchive.Core.Domain.Aggregates.Role;
using ScienceArchive.Core.Domain.Aggregates.User;

namespace ScienceArchive.Application;

public static class ApplicationRegistry
{
    /// <summary>
    /// Register all required application layer services 
    /// </summary>
    /// <param name="services">Instance of <see cref="IServiceCollection"/></param>
    public static IServiceCollection RegisterApplicationLayer(this IServiceCollection services)
    {
        return services
            .RegisterApplicationMappers()
            .RegisterMediatR()
            .RegisterApplicationServices();
    }

    /// <summary>
    /// Register application layer services
    /// </summary>
    /// <param name="services">System services</param>
    /// <returns>System services with registered interactors</returns>
    private static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
    {
        return services
            .AddTransient<IArticleApplicationService, ArticleApplicationService>()
            .AddTransient<IAuthApplicationService, AuthApplicationService>()
            .AddTransient<ICategoryApplicationService, CategoryApplicationService>()
            .AddTransient<ILogApplicationService, LogApplicationService>()
            .AddTransient<INewsApplicationService, NewsApplicationService>()
            .AddTransient<IRoleApplicationService, RoleApplicationService>()
            .AddTransient<ISystemApplicationService, SystemApplicationService>()
            .AddTransient<IUserApplicationService, UserApplicationService>();
    }

    /// <summary>
    /// Register application layer mappers
    /// </summary>
    /// <param name="services">System services</param>
    /// <returns>System services with registered application layer mappers</returns>
    private static IServiceCollection RegisterApplicationMappers(this IServiceCollection services)
    {
        return services
            .AddTransient<IApplicationMapper<Article, ArticleDto>, ArticleMapper>()
            .AddTransient<IApplicationMapper<Category, CategoryDto>, CategoryMapper>()
            .AddTransient<IApplicationMapper<Subcategory, CategoryDto>, SubcategoryMapper>()
            .AddTransient<IApplicationMapper<News, NewsDto>, NewsMapper>()
            .AddTransient<IApplicationMapper<Role, RoleDto>, RoleMapper>()
            .AddTransient<IApplicationMapper<User, UserDto>, UserMapper>();
    }

    /// <summary>
    /// Register all required application use cases.
    /// </summary>
    /// <returns>Instance of <see cref="IServiceCollection"/> after registering the use cases.</returns>
    private static IServiceCollection RegisterMediatR(this IServiceCollection services)
    {
        return services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()); });
    }
}