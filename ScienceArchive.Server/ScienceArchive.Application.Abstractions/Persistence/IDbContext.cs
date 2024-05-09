using ScienceArchive.Core.Domain.Aggregates.Article.Repositories;
using ScienceArchive.Core.Domain.Aggregates.Category.Repositories;
using ScienceArchive.Core.Domain.Aggregates.News.Repositories;
using ScienceArchive.Core.Domain.Aggregates.Notification.Repositories;
using ScienceArchive.Core.Domain.Aggregates.Role.Repositories;
using ScienceArchive.Core.Domain.Aggregates.User.Repositories;

namespace ScienceArchive.Application.Abstractions.Persistence;

/// <summary>
/// Represents the context for working with various repositories.
/// </summary>
public interface IDbContext
{
    /// <summary>
    /// Represents a repository for managing articles.
    /// </summary>
    public IArticleRepository ArticleRepository { get; }

    /// <summary>
    /// Represents a repository for managing categories.
    /// </summary>
    public ICategoryRepository CategoryRepository { get; }

    /// <summary>
    /// Represents a repository for managing news.
    /// </summary>
    public INewsRepository NewsRepository { get; }

    /// <summary>
    /// Represents a repository for managing notifications.
    /// </summary>
    public INotificationRepository NotificationRepository { get; }

    /// <summary>
    /// RoleRepository interface represents a repository for managing roles.
    /// </summary>
    public IRoleRepository RoleRepository { get; }

    /// <summary>
    /// User repository functionality.
    /// </summary>
    public IUserRepository UserRepository { get; }
    
    /// <summary>
    /// Saves changes made to the context asynchronously.
    /// </summary>
    public Task SaveAsync();

    /// <summary>
    /// Rolls back changes made to the context asynchronously.
    /// </summary>
    public Task RollbackAsync();
}