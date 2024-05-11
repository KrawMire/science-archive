using ScienceArchive.Core.Domain.Aggregates.Article;
using ScienceArchive.Core.Domain.Aggregates.Article.Repositories;
using ScienceArchive.Core.Domain.Aggregates.Article.ValueObjects;
using ScienceArchive.Core.Domain.Aggregates.Category.ValueObjects;
using ScienceArchive.Core.Domain.Aggregates.User.ValueObjects;
using ScienceArchive.Infrastructure.Interfaces;
using ScienceArchive.Infrastructure.Persistence.PostgreSql.Models;

namespace ScienceArchive.Infrastructure.Persistence.PostgreSql.Repositories;

internal class PostgresArticleRepository : IArticleRepository
{
    private readonly IInfrastructureMapper<Article, ArticleModel> _mapper;
    private readonly PostgresExecutionContext _dbContext;
    
    public PostgresArticleRepository(
        PostgresExecutionContext dbContext,
        IInfrastructureMapper<Article, ArticleModel> mapper)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public Task<List<Article>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<List<Article>> GetAllVerified()
    {
        throw new NotImplementedException();
    }

    public Task<List<Article>> GetVerifiedByCategoryId(CategoryId categoryId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Article>> GetByAuthorId(UserId userId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Article>> GetVerifiedByAuthorId(UserId userId)
    {
        throw new NotImplementedException();
    }

    public Task<Article?> GetById(ArticleId id)
    {
        throw new NotImplementedException();
    }

    public Task<Article> Create(Article newValue)
    {
        throw new NotImplementedException();
    }

    public Task<Article> Update(ArticleId id, Article newValue)
    {
        throw new NotImplementedException();
    }
    
    public Task<ArticleId> Delete(ArticleId id)
    {
        throw new NotImplementedException();
    }
}