using ScienceArchive.Application.Abstractions.Persistence;
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
internal class PostgresDbContext : IDbContext, IDisposable, IAsyncDisposable
{
    private readonly PostgresExecutionContext _context;
    
    public PostgresDbContext(
        PostgresExecutionContext context,
        IArticleRepository articleRepository, 
        ICategoryRepository categoryRepository, 
        INewsRepository newsRepository, 
        INotificationRepository notificationRepository, 
        IRoleRepository roleRepository, 
        IUserRepository userRepository)
    {
        _context = context;
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
        if (_context.Transaction is not null)
        {
            throw new InvalidOperationException("Transaction has already been started");
        }
        
        await _context.Connection.OpenAsync();
        _context.Transaction = await _context.Connection.BeginTransactionAsync();
    }

    /// <inheritdoc/>
    public async Task SaveAsync()
    {
        if (_context.Transaction is null)
        {
            throw new InvalidOperationException("There is no transaction to commit");
        }
        
        await _context.Transaction.CommitAsync();
        await _context.Transaction.DisposeAsync();
        
        _context.Transaction = null;
    }

    /// <inheritdoc/>
    public async Task RollbackAsync()
    {
        if (_context.Transaction is null)
        {
            throw new InvalidOperationException("There is no transaction to rollback");
        }
        
        await _context.Transaction.RollbackAsync();
        await _context.Transaction.DisposeAsync();
        
        _context.Transaction = null;
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        _context.Connection.Dispose();

        if (_context.Transaction is not null)
        {
            _context.Transaction.Rollback();
            _context.Transaction.Dispose();   
        }
    }

    /// <inheritdoc/>
    public async ValueTask DisposeAsync()
    {
        await _context.Connection.DisposeAsync();

        if (_context.Transaction is not null)
        {
            await _context.Transaction.RollbackAsync();
            await _context.Transaction.DisposeAsync();   
        }
    }
}