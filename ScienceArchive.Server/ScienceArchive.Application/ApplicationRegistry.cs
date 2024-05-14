using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using ScienceArchive.Application.Dtos.Article;
using ScienceArchive.Application.Dtos.Article.Request;
using ScienceArchive.Application.Dtos.Article.Response;
using ScienceArchive.Application.Dtos.Auth.Request;
using ScienceArchive.Application.Dtos.Auth.Response;
using ScienceArchive.Application.Dtos.Category;
using ScienceArchive.Application.Dtos.Category.Request;
using ScienceArchive.Application.Dtos.Category.Response;
using ScienceArchive.Application.Dtos.Log.Request;
using ScienceArchive.Application.Dtos.Log.Response;
using ScienceArchive.Application.Dtos.News;
using ScienceArchive.Application.Dtos.News.Request;
using ScienceArchive.Application.Dtos.News.Response;
using ScienceArchive.Application.Dtos.Role;
using ScienceArchive.Application.Dtos.Role.Request;
using ScienceArchive.Application.Dtos.Role.Response;
using ScienceArchive.Application.Dtos.System.Request;
using ScienceArchive.Application.Dtos.System.Response;
using ScienceArchive.Application.Dtos.User;
using ScienceArchive.Application.Dtos.User.Request;
using ScienceArchive.Application.Dtos.User.Response;
using ScienceArchive.Application.Events;
using ScienceArchive.Application.Events.EventWrappers;
using ScienceArchive.Application.Events.Handlers;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Application.Interfaces.Services;
using ScienceArchive.Application.Mappers;
using ScienceArchive.Application.Services;
using ScienceArchive.Application.UseCases.ArticleUseCases;
using ScienceArchive.Application.UseCases.AuthUseCases;
using ScienceArchive.Application.UseCases.CategoryUseCases;
using ScienceArchive.Application.UseCases.LogUseCases;
using ScienceArchive.Application.UseCases.NewsUseCases;
using ScienceArchive.Application.UseCases.RoleUseCases;
using ScienceArchive.Application.UseCases.SystemUseCases;
using ScienceArchive.Application.UseCases.UserUseCases;
using ScienceArchive.Core.Domain.Aggregates.Article;
using ScienceArchive.Core.Domain.Aggregates.Category;
using ScienceArchive.Core.Domain.Aggregates.Category.Entities;
using ScienceArchive.Core.Domain.Aggregates.News;
using ScienceArchive.Core.Domain.Aggregates.Role;
using ScienceArchive.Core.Domain.Aggregates.User;
using ScienceArchive.Core.Domain.Events;

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
            .RegisterEventHandlers()
            .RegisterEventBus()
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
        return services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
    }

    /// <summary>
    /// Register the event bus implementation for handling events
    /// </summary>
    /// <returns>System services with registered event bus</returns>
    private static IServiceCollection RegisterEventBus(this IServiceCollection services)
    {
        return services.AddScoped<IEventBus, EventBus>();
    }

    /// <summary>
    /// Registers the event handlers for domain events.
    /// </summary>
    /// <returns>Instance of <see cref="IServiceCollection"/>.</returns>
    private static IServiceCollection RegisterEventHandlers(this IServiceCollection services)
    {
        return services
            .AddTransient<IEventHandler<ArticleStatusChangedEventWrapper, ArticleStatusChangedEvent>, ArticleStatusChangedEmailEventHandler>()
            .AddTransient<IEventHandler<UserRegisteredEventWrapper, UserRegisteredEvent>, UserRegisteredEmailEventHandler>();
    }
}