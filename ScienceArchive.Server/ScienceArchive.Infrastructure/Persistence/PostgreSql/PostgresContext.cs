using Npgsql;
using ScienceArchive.Application.Abstractions.Persistence;
using ScienceArchive.Core.Domain.Aggregates.Article.Repositories;
using ScienceArchive.Core.Domain.Aggregates.Category.Repositories;
using ScienceArchive.Core.Domain.Aggregates.News.Repositories;
using ScienceArchive.Core.Domain.Aggregates.Notification.Repositories;
using ScienceArchive.Core.Domain.Aggregates.Role.Repositories;
using ScienceArchive.Core.Domain.Aggregates.User.Repositories;
using ScienceArchive.Infrastructure.Persistence.PostgreSql.Options;

namespace ScienceArchive.Infrastructure.Persistence.PostgreSql;

/// <summary>
/// Context of connection to PostgreSQL
/// </summary>
internal class PostgresDbContext : IDbContext, IDisposable, IAsyncDisposable
{
    public PostgresDbContext(
        PostgresConnectionOptions connectionOptions,
        IArticleRepository articleRepository, 
        ICategoryRepository categoryRepository, 
        INewsRepository newsRepository, 
        INotificationRepository notificationRepository, 
        IRoleRepository roleRepository, 
        IUserRepository userRepository)
    {
        ArticleRepository = articleRepository;
        CategoryRepository = categoryRepository;
        NewsRepository = newsRepository;
        NotificationRepository = notificationRepository;
        RoleRepository = roleRepository;
        UserRepository = userRepository;

        Connection = new NpgsqlConnection(connectionOptions.PostgresConnectionString);
    }

    /// <inheritdoc/>
    public IArticleRepository ArticleRepository { get; }
    
    /// <inheritdoc/>
    public ICategoryRepository CategoryRepository { get; }
    
    /// <inheritdoc/>
    public INewsRepository NewsRepository { get; }
    
    /// <inheritdoc/>
    public INotificationRepository NotificationRepository { get; }
    
    /// <inheritdoc/>
    public IRoleRepository RoleRepository { get; }
    
    /// <inheritdoc/>
    public IUserRepository UserRepository { get; }

    /// <summary>
    /// Represents the connection to the PostgreSQL database.
    /// </summary>
    public NpgsqlConnection Connection { get; }

    /// <summary>
    /// Represents a transaction within a PostgreSQL database context.
    /// </summary>
    public NpgsqlTransaction? Transaction { get; private set; }

    /// <inheritdoc/>
    public async Task StartTransactionAsync()
    {
        if (Transaction is not null)
        {
            throw new InvalidOperationException("Transaction has already been started");
        }

        Transaction = await Connection.BeginTransactionAsync();
    }

    /// <inheritdoc/>
    public async Task SaveAsync()
    {
        if (Transaction is null)
        {
            throw new InvalidOperationException("There is no transaction to commit");
        }
        
        await Transaction.CommitAsync();
        await Transaction.DisposeAsync();
        
        Transaction = null;
    }

    /// <inheritdoc/>
    public async Task RollbackAsync()
    {
        if (Transaction is null)
        {
            throw new InvalidOperationException("There is no transaction to rollback");
        }
        
        await Transaction.RollbackAsync();
        await Transaction.DisposeAsync();
        
        Transaction = null;
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        Connection.Dispose();

        if (Transaction is not null)
        {
            Transaction.Rollback();
            Transaction.Dispose();   
        }
    }

    /// <inheritdoc/>
    public async ValueTask DisposeAsync()
    {
        await Connection.DisposeAsync();

        if (Transaction is not null)
        {
            await Transaction.RollbackAsync();
            await Transaction.DisposeAsync();   
        }
    }
}