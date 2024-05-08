using Microsoft.Extensions.DependencyInjection;
using ScienceArchive.Application.Dtos;
using ScienceArchive.Application.Dtos.Article;
using ScienceArchive.Application.Dtos.Category;
using ScienceArchive.Application.Dtos.Claim;
using ScienceArchive.Application.Dtos.News;
using ScienceArchive.Application.Dtos.Role;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Application.Interfaces.Services;
using ScienceArchive.Application.Mappers;
using ScienceArchive.Application.Services;
using ScienceArchive.Core.Domain.Aggregates.Article;
using ScienceArchive.Core.Domain.Aggregates.Category;
using ScienceArchive.Core.Domain.Aggregates.News;
using ScienceArchive.Core.Domain.Aggregates.Role;
using ScienceArchive.Core.Domain.Aggregates.Role.ValueObjects;
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
            .RegisterApplicationServices()
            .RegisterApplicationMappers();
    }
    
    /// <summary>
    /// Register application layer interactors
    /// </summary>
    /// <param name="services">System services</param>
    /// <returns>System services with registered interactors</returns>
    private static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
    {
        return services
            .AddSingleton<IArticleApplicationService, ArticleApplicationService>()
            .AddSingleton<IAuthApplicationService, AuthApplicationService>()
            .AddSingleton<ICategoryApplicationService, CategoryApplicationService>()
            .AddSingleton<INewsApplicationService, NewsApplicationService>()
            .AddSingleton<IRoleApplicationService, RoleApplicationService>()
            .AddSingleton<ISystemApplicationService, SystemApplicationService>()
            .AddSingleton<IUserApplicationService, UserApplicationService>();
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
            .AddTransient<IApplicationMapper<RoleClaim, ClaimDto>, ClaimMapper>()
            .AddTransient<IApplicationMapper<News, NewsDto>, NewsMapper>()
            .AddTransient<IApplicationMapper<Role, RoleDto>, RoleMapper>()
            .AddTransient<IApplicationMapper<User, UserDto>, UserMapper>();
            // .AddTransient<IApplicationMapper<Author, AuthorDto>, AuthorMapper>();
    }
}