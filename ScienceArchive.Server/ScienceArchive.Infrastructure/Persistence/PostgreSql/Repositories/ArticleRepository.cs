using Dapper;
using System.Data;
using System.Text.Json;
using ScienceArchive.Core.Domain.Aggregates.Article;
using ScienceArchive.Core.Domain.Aggregates.Article.Repositories;
using ScienceArchive.Core.Domain.Aggregates.Article.ValueObjects;
using ScienceArchive.Core.Domain.Aggregates.Category.ValueObjects;
using ScienceArchive.Core.Domain.Aggregates.User.ValueObjects;
using ScienceArchive.Core.Exceptions;
using ScienceArchive.Infrastructure.Interfaces;
using ScienceArchive.Infrastructure.Persistence.Exceptions;
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

    public async Task<List<Article>> GetAll()
    {
        var articles = await _dbContext.Connection.QueryAsync<ArticleModel>(
            "SELECT * FROM func_get_all_articles()",
            commandType: CommandType.Text,
            transaction: _dbContext.Transaction);

        if (articles is null)
        {
            throw new EntityNotFoundException(nameof(Article));
        }

        return articles.Select(article => _mapper.MapToEntity(article)).ToList();
    }

    public async Task<List<Article>> GetAllVerified()
    {
        var articles = await _dbContext.Connection.QueryAsync<ArticleModel>(
            "SELECT * FROM func_get_all_verified_articles()",
            commandType: CommandType.Text,
            transaction: _dbContext.Transaction);

        if (articles is null)
        {
            throw new EntityNotFoundException(nameof(Article));
        }

        return articles.Select(article => _mapper.MapToEntity(article)).ToList();
    }

    public async Task<List<Article>> GetVerifiedByCategoryId(CategoryId categoryId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("CategoryId", categoryId.Value);

        var articles = await _dbContext.Connection.QueryAsync<ArticleModel>(
            "SELECT * FROM func_get_verified_articles_by_category_id(@CategoryId::uuid)",
            parameters,
            commandType: CommandType.Text,
            transaction: _dbContext.Transaction);

        if (articles is null)
        {
            throw new EntityNotFoundException(nameof(Article));
        }

        return articles.Select(_mapper.MapToEntity).ToList();
    }

    public async Task<List<Article>> GetByAuthorId(UserId userId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("UserId", userId.Value);

        var articles = await _dbContext.Connection.QueryAsync<ArticleModel>(
            "SELECT * FROM func_get_articles_by_author_id(@UserId::uuid)",
            parameters,
            commandType: CommandType.Text,
            transaction: _dbContext.Transaction);

        if (articles is null)
        {
            throw new EntityNotFoundException(nameof(Article));
        }

        return articles.Select(_mapper.MapToEntity).ToList();
    }

    public async Task<List<Article>> GetVerifiedByAuthorId(UserId userId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("UserId", userId.Value);

        var articles = await _dbContext.Connection.QueryAsync<ArticleModel>(
            "SELECT * FROM func_get_verified_articles_by_author_id(@UserId::uuid)",
            parameters,
            commandType: CommandType.Text,
            transaction: _dbContext.Transaction);

        if (articles is null)
        {
            throw new EntityNotFoundException(nameof(Article));
        }

        return articles.Select(_mapper.MapToEntity).ToList();
    }

    public async Task<Article?> GetById(ArticleId id)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", id.Value);

        var article = await _dbContext.Connection.QueryFirstOrDefaultAsync<ArticleModel?>(
            "SELECT * FROM func_get_article_by_id(@Id::uuid)",
            parameters,
            commandType: CommandType.Text,
            transaction: _dbContext.Transaction);

        return article is not null ? _mapper.MapToEntity(article) : null;
    }

    public async Task<Article> Create(Article newValue)
    {
        var articleToCreate = _mapper.MapToModel(newValue);
        var parameters = new DynamicParameters(articleToCreate);
        parameters.Add("Documents", JsonSerializer.Serialize(articleToCreate.Documents));

        var sql = @"SELECT * FROM func_create_article(
            @Id::uuid, 
            @CategoryId::uuid, 
            @Title::varchar(255), 
            @Description, 
            @CreationDate, 
            @AuthorsIds, 
            @Documents::jsonb, 
            @Status)";
        
        var createdArticle = await _dbContext.Connection.QueryFirstOrDefaultAsync<ArticleModel>(
            sql,
            parameters,
            commandType: CommandType.Text,
            transaction: _dbContext.Transaction);
        
        if (createdArticle is null)
        {
            throw new PersistenceException("New article was not created!");
        }

        return _mapper.MapToEntity(createdArticle);
    }

    public async Task<Article> Update(ArticleId id, Article newValue)
    {
        var articleToUpdate = _mapper.MapToModel(newValue);
        var parameters = new DynamicParameters(articleToUpdate);
        parameters.Add("Id", id.Value);
        parameters.Add("Documents", JsonSerializer.Serialize(articleToUpdate.Documents));

        var sql = @"SELECT * FROM func_update_article(
            @Id::uuid,
            @CategoryId::uuid, 
            @Title::varchar(255), 
            @Description::text, 
            @AuthorsIds::uuid[], 
            @Documents::jsonb, 
            @Status::int)";
        
        var updatedArticle = await _dbContext.Connection.QueryFirstOrDefaultAsync<ArticleModel>(
            sql,
            parameters,
            commandType: CommandType.Text,
            transaction: _dbContext.Transaction);

        if (updatedArticle is null)
        {
            throw new PersistenceException("Article was not updated!");
        }

        return _mapper.MapToEntity(updatedArticle);
    }
    
    public async Task<ArticleId> Delete(ArticleId id)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", id.Value);

        var deletedArticleId = await _dbContext.Connection.QueryFirstOrDefaultAsync<Guid>(
            "SELECT * FROM func_delete_article(@Id::uuid)",
            parameters,
            commandType: CommandType.Text,
            transaction: _dbContext.Transaction);

        if (deletedArticleId == default)
        {
            throw new PersistenceException("Article was not deleted");
        }
            
        return ArticleId.CreateFromGuid(deletedArticleId);
    }
}