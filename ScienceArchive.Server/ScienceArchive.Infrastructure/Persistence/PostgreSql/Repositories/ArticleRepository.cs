using Microsoft.EntityFrameworkCore;
using ScienceArchive.Core.Domain.Aggregates.Article.Enums;
using ScienceArchive.Core.Domain.Aggregates.Article.Factories;
using ScienceArchive.Core.Domain.Aggregates.Article.Repositories;
using ScienceArchive.Core.Domain.Aggregates.Article.ValueObjects;
using ScienceArchive.Core.Domain.Aggregates.Category.ValueObjects;
using ScienceArchive.Core.Domain.Aggregates.User.ValueObjects;
using ScienceArchive.Core.Exceptions;
using ScienceArchive.Infrastructure.Persistence.Exceptions;
using ScienceArchive.Infrastructure.Persistence.PostgreSql.Entities;
using Article = ScienceArchive.Core.Domain.Aggregates.Article.Article;
using Category = ScienceArchive.Core.Domain.Aggregates.Category.Category;
using User = ScienceArchive.Core.Domain.Aggregates.User.User;

namespace ScienceArchive.Infrastructure.Persistence.PostgreSql.Repositories;

internal class PostgresArticleRepository : IArticleRepository
{
    private readonly PostgresDbContext _dbContext;
    
    public PostgresArticleRepository(PostgresDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<List<Article>> GetAll()
    {
        var articles = await _dbContext
            .Articles
            .Include(a => a.UsersArticles)
            .ThenInclude(ua => ua.User)
            .Include(a => a.Category)
            .Include(a => a.ArticlesDocuments)
            .OrderByDescending(a => a.CreationDate)
            .ToListAsync();

        return articles
            .Select(a =>
            {
                var builder = new ArticleBuilder(a.Id);

                foreach (var userArticle in a.UsersArticles)
                {
                    builder.AddAuthor(userArticle.UserId, userArticle.User.Name, userArticle.Role);
                }

                foreach (var articleDocument in a.ArticlesDocuments)
                {
                    builder.AddDocument(articleDocument.Id, articleDocument.Name, articleDocument.Filepath);
                }
                
                return builder
                    .AddTitle(a.Title)
                    .AddStatus(a.Status)
                    .AddCategory(a.CategoryId, a.Category.Name)
                    .AddCreationDate(a.CreationDate)
                    .AddDescription(a.Description)
                    .Build();
            }).ToList();
    }

    public async Task<List<Article>> GetAllVerified()
    {
        var articles = await _dbContext
            .Articles
            .Where(a => a.Status == (short)ArticleStatus.Verified)
            .Include(a => a.UsersArticles)
            .ThenInclude(ua => ua.User)
            .Include(a => a.Category)
            .Include(a => a.ArticlesDocuments)
            .OrderByDescending(a => a.CreationDate)
            .ToListAsync();

        return articles
            .Select(a =>
            {
                var builder = new ArticleBuilder(a.Id);

                foreach (var userArticle in a.UsersArticles)
                {
                    builder.AddAuthor(userArticle.UserId, userArticle.User.Name, userArticle.Role);
                }

                foreach (var articleDocument in a.ArticlesDocuments)
                {
                    builder.AddDocument(articleDocument.Id, articleDocument.Name, articleDocument.Filepath);
                }
                
                return builder
                    .AddTitle(a.Title)
                    .AddStatus(a.Status)
                    .AddCategory(a.CategoryId, a.Category.Name)
                    .AddCreationDate(a.CreationDate)
                    .AddDescription(a.Description)
                    .Build();
            }).ToList();
    }

    public async Task<List<Article>> GetVerifiedBySubcategoryId(SubcategoryId categoryId)
    {
        var category = await _dbContext
            .Subcategories
            .Where(c => c.Id == categoryId.Value)
            .FirstOrDefaultAsync();

        if (category is null)
        {
            throw new EntityNotFoundException(nameof(Category));
        }
        
        var articles = await _dbContext
            .Articles
            .Where(a => a.CategoryId == category.Id)
            .Include(a => a.UsersArticles)
            .ThenInclude(ua => ua.User)
            .Include(a => a.Category)
            .Include(a => a.ArticlesDocuments)
            .OrderByDescending(a => a.CreationDate)
            .ToListAsync();

        return articles
            .Select(a =>
            {
                var builder = new ArticleBuilder(a.Id);

                foreach (var userArticle in a.UsersArticles)
                {
                    builder.AddAuthor(userArticle.UserId, userArticle.User.Name, userArticle.Role);
                }

                foreach (var articleDocument in a.ArticlesDocuments)
                {
                    builder.AddDocument(articleDocument.Id, articleDocument.Name, articleDocument.Filepath);
                }
                
                return builder
                    .AddTitle(a.Title)
                    .AddStatus(a.Status)
                    .AddCategory(a.CategoryId, a.Category.Name)
                    .AddCreationDate(a.CreationDate)
                    .AddDescription(a.Description)
                    .Build();
            }).ToList();
    }

    public async Task<List<Article>> GetByAuthorId(UserId userId)
    {
        var author = await _dbContext
            .Users
            .Where(u => u.Id == userId.Value)
            .FirstOrDefaultAsync();

        if (author is null)
        {
            throw new EntityNotFoundException(nameof(User));
        }
        
        var articles = await _dbContext
            .Articles
            .Include(a => a.UsersArticles)
            .ThenInclude(ua => ua.User)
            .Where(a => a.UsersArticles.Any(ua => ua.UserId == userId.Value))
            .Include(a => a.Category)
            .Include(a => a.ArticlesDocuments)
            .OrderByDescending(a => a.CreationDate)
            .ToListAsync();

        return articles
            .Select(a =>
            {
                var builder = new ArticleBuilder(a.Id);

                foreach (var userArticle in a.UsersArticles)
                {
                    builder.AddAuthor(userArticle.UserId, userArticle.User.Name, userArticle.Role);
                }

                foreach (var articleDocument in a.ArticlesDocuments)
                {
                    builder.AddDocument(articleDocument.Id, articleDocument.Name, articleDocument.Filepath);
                }
                
                return builder
                    .AddTitle(a.Title)
                    .AddStatus(a.Status)
                    .AddCategory(a.CategoryId, a.Category.Name)
                    .AddCreationDate(a.CreationDate)
                    .AddDescription(a.Description)
                    .Build();
            }).ToList();
    }

    public async Task<List<Article>> GetVerifiedByAuthorId(UserId userId)
    {
        var author = await _dbContext
            .Users
            .Where(u => u.Id == userId.Value)
            .FirstOrDefaultAsync();

        if (author is null)
        {
            throw new EntityNotFoundException(nameof(User));
        }

        var articles = await _dbContext
            .Articles
            .Include(a => a.UsersArticles)
            .ThenInclude(ua => ua.User)
            .Where(a => a.UsersArticles.Any(ua => ua.UserId == userId.Value) 
                        && a.Status == (short)ArticleStatus.Verified)
            .Include(a => a.Category)
            .Include(a => a.ArticlesDocuments)
            .OrderByDescending(a => a.CreationDate)
            .ToListAsync();

        return articles
            .Select(a =>
            {
                var builder = new ArticleBuilder(a.Id);

                foreach (var userArticle in a.UsersArticles)
                {
                    builder.AddAuthor(userArticle.UserId, userArticle.User.Name, userArticle.Role);
                }

                foreach (var articleDocument in a.ArticlesDocuments)
                {
                    builder.AddDocument(articleDocument.Id, articleDocument.Name, articleDocument.Filepath);
                }
                
                return builder
                    .AddTitle(a.Title)
                    .AddStatus(a.Status)
                    .AddCategory(a.CategoryId, a.Category.Name)
                    .AddCreationDate(a.CreationDate)
                    .AddDescription(a.Description)
                    .Build();
            }).ToList();
    }

    public async Task<Article?> GetById(ArticleId id)
    {
        var article = await _dbContext
            .Articles
            .Where(a => a.Id == id.Value)
            .Include(a => a.UsersArticles)
            .ThenInclude(ua => ua.User)
            .Include(a => a.Category)
            .Include(a => a.ArticlesDocuments)
            .FirstOrDefaultAsync();

        if (article is null)
        {
            return null;
        }
        
        var builder = new ArticleBuilder(article.Id);

        foreach (var userArticle in article.UsersArticles)
        {
            builder.AddAuthor(userArticle.UserId, userArticle.User.Name, userArticle.Role);
        }

        foreach (var articleDocument in article.ArticlesDocuments)
        {
            builder.AddDocument(articleDocument.Id, articleDocument.Name, articleDocument.Filepath);
        }
                
        return builder
            .AddTitle(article.Title)
            .AddStatus(article.Status)
            .AddCategory(article.CategoryId, article.Category.Name)
            .AddCreationDate(article.CreationDate)
            .AddDescription(article.Description)
            .Build();
    }

    public async Task<Article> Create(Article newValue)
    {
        await _dbContext
            .ArticlesDocuments
            .AddRangeAsync(newValue
                .Documents
                .Select(d => new ArticlesDocument
                {
                    Id = d.Id.Value,
                    ArticleId = newValue.Id.Value,
                    Name = d.Name,
                    Filepath = d.Path
                }));

        await _dbContext
            .UsersArticles
            .AddRangeAsync(newValue
                .Authors
                .Select(a => new UsersArticle
                {
                    ArticleId = newValue.Id.Value,
                    UserId = a.UserId.Value,
                    Role = (short)a.Role
                }));
        
        var article = await _dbContext
            .Articles
            .AddAsync(new Entities.Article
            {
                Id = newValue.Id.Value,
                CategoryId = newValue.Category.CategoryId.Value,
                Title = newValue.Title,
                Status = (short)newValue.Status,
                CreationDate = newValue.CreationDate,
                Description = newValue.Description
            });
        
        await _dbContext.SaveChangesAsync();

        var createdArticle = await GetById(ArticleId.CreateFromGuid(article.Entity.Id));

        if (createdArticle is null)
        {
            throw new PersistenceException("Article was not created");
        }
        
        return createdArticle;
    }

    public async Task<Article> Update(ArticleId id, Article newValue)
    {
        var article = await _dbContext
            .Articles
            .Where(a => a.Id == id.Value)
            .FirstOrDefaultAsync();

        if (article is null)
        {
            throw new EntityNotFoundException(nameof(Article));
        }
        
        article.CategoryId = newValue.Category.CategoryId.Value;
        article.Title = newValue.Title;
        article.Status = (short)newValue.Status;
        article.CreationDate = newValue.CreationDate;
        article.Description = newValue.Description;
        
        _dbContext
            .ArticlesDocuments
            .RemoveRange(_dbContext
                .ArticlesDocuments
                .Where(ad => ad.ArticleId == id.Value));
        _dbContext
            .UsersArticles
            .RemoveRange(_dbContext
                .UsersArticles
                .Where(ua => ua.ArticleId == id.Value));
        
        await _dbContext
            .ArticlesDocuments
            .AddRangeAsync(newValue
                .Documents
                .Select(ad => new ArticlesDocument
                {
                    Id = ad.Id.Value,
                    ArticleId = id.Value,
                    Name = ad.Name,
                    Filepath = ad.Path
                }));
        
        await _dbContext
            .UsersArticles
            .AddRangeAsync(newValue
                .Authors
                .Select(ua => new UsersArticle()
                {
                    ArticleId = id.Value,
                    UserId = ua.UserId.Value,
                    Role = (short)ua.Role
                }));
        
        await _dbContext.SaveChangesAsync();

        return (await GetById(id))!;
    }
    
    public async Task<ArticleId> Delete(ArticleId id)
    {
        var article = await _dbContext
            .Articles
            .Where(a => a.Id == id.Value)
            .FirstOrDefaultAsync();

        if (article is null)
        {
            throw new EntityNotFoundException(nameof(Article));
        }
        
        _dbContext
            .ArticlesDocuments
            .RemoveRange(_dbContext
                .ArticlesDocuments
                .Where(ad => ad.ArticleId == id.Value));
        _dbContext
            .UsersArticles
            .RemoveRange(_dbContext
                .UsersArticles
                .Where(ua => ua.ArticleId == id.Value));

        _dbContext
            .Articles
            .Remove(article);

        await _dbContext.SaveChangesAsync();
        return id;
    }
}