using ScienceArchive.Shared.Abstractions.Persistence;
using ScienceArchive.Core.Domain.Aggregates.Article.Repositories;
using ScienceArchive.Core.Domain.Aggregates.Category.Repositories;
using ScienceArchive.Core.Domain.Aggregates.News.Repositories;
using ScienceArchive.Core.Domain.Aggregates.Notification.Repositories;
using ScienceArchive.Core.Domain.Aggregates.Role.Repositories;
using ScienceArchive.Core.Domain.Aggregates.User.Repositories;

namespace ScienceArchive.Infrastructure.Persistence.PostgreSql;

/// <summary>
/// Context of connection to PostgreSQL
/// </summary>
internal class PostgresDbUnitOfWork : IDbUnitOfWork, IDisposable, IAsyncDisposable
{
    private readonly PostgresDbContext _dbContext;
    private readonly PostgresDbExecutionContext _executionContext;
    
    public PostgresDbUnitOfWork(
        PostgresDbContext context,
        PostgresDbExecutionContext executionContext,
        IArticleRepository articleRepository, 
        ICategoryRepository categoryRepository, 
        INewsRepository newsRepository, 
        INotificationRepository notificationRepository, 
        IRoleRepository roleRepository, 
        IUserRepository userRepository)
    {
        _dbContext = context;
        _executionContext = executionContext;
        ArticleRepository = articleRepository;
        CategoryRepository = categoryRepository;
        NewsRepository = newsRepository;
        NotificationRepository = notificationRepository;
        RoleRepository = roleRepository;
        UserRepository = userRepository;
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

    /// <inheritdoc/>
    public async Task StartTransactionAsync()
    {
        if (_dbContext.Database.CurrentTransaction is not null)
        {
            throw new InvalidOperationException("Transaction has already been started");
        }
        
        _executionContext.Transaction = await _dbContext.Database.BeginTransactionAsync();
    }

    /// <inheritdoc/>
    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
        
        if (_executionContext.Transaction is null)
        {
            throw new InvalidOperationException("There is no transaction to commit");
        }
        
        await _executionContext.Transaction.CommitAsync();
        await _executionContext.Transaction.DisposeAsync();
        
        _executionContext.Transaction = null;
    }

    /// <inheritdoc/>
    public async Task RollbackAsync()
    {
        if (_executionContext.Transaction is null)
        {
            throw new InvalidOperationException("There is no transaction to rollback");
        }
        
        await _executionContext.Transaction.RollbackAsync();
        await _executionContext.Transaction.DisposeAsync();
        
        _executionContext.Transaction = null;
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        _dbContext.Dispose();
        
        if (_executionContext.Transaction is not null)
        {
            _executionContext.Transaction.Rollback();
            _executionContext.Transaction.Dispose();   
        }
    }

    /// <inheritdoc/>
    public async ValueTask DisposeAsync()
    {
        await _dbContext.DisposeAsync();
        
        if (_executionContext.Transaction is not null)
        {
            await _executionContext.Transaction.RollbackAsync();
            await _executionContext.Transaction.DisposeAsync();   
        }
    }
}