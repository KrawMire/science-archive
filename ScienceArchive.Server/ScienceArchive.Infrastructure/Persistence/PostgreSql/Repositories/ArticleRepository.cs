﻿using Dapper;
using System.Data;
using System.Text.Json;
using ScienceArchive.Core.Domain.Aggregates.Article;
using ScienceArchive.Core.Domain.Aggregates.Article.ValueObjects;
using ScienceArchive.Core.Domain.Aggregates.Category.ValueObjects;
using ScienceArchive.Core.Domain.Aggregates.User.ValueObjects;
using ScienceArchive.Core.Repositories;
using ScienceArchive.Infrastructure.Persistence.Exceptions;
using ScienceArchive.Infrastructure.Persistence.Interfaces;
using ScienceArchive.Infrastructure.Persistence.PostgreSql.Models;

namespace ScienceArchive.Infrastructure.Persistence.PostgreSql.Repositories;

internal class PostgresArticleRepository : IArticleRepository
{
    private readonly IDbConnection _connection;
    private readonly IPersistenceMapper<Article, ArticleModel> _mapper;
        
    public PostgresArticleRepository(PostgresContext dbContext, IPersistenceMapper<Article, ArticleModel> mapper)
    {
        var context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _connection = context.CreateConnection();
    }

    public async Task<List<Article>> GetAll()
    {
        var articles = await _connection.QueryAsync<ArticleModel>(
            "SELECT * FROM func_get_all_articles()",
            commandType: CommandType.Text);

        if (articles is null)
        {
            throw new EntityNotFoundException<NewsModel>("Cannot get any article");
        }

        return articles.Select(article => _mapper.MapToEntity(article)).ToList();
    }

    public async Task<List<Article>> GetAllVerified()
    {
        var articles = await _connection.QueryAsync<ArticleModel>(
            "SELECT * FROM func_get_all_verified_articles()",
            commandType: CommandType.Text);

        if (articles is null)
        {
            throw new EntityNotFoundException<NewsModel>("Cannot get any article");
        }

        return articles.Select(article => _mapper.MapToEntity(article)).ToList();
    }

    public async Task<List<Article>> GetVerifiedByCategoryId(CategoryId categoryId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("CategoryId", categoryId.Value);

        var articles = await _connection.QueryAsync<ArticleModel>(
            "SELECT * FROM func_get_verified_articles_by_category_id(@CategoryId::uuid)",
            parameters,
            commandType: CommandType.Text);

        if (articles is null)
        {
            throw new EntityNotFoundException<NewsModel>("Cannot get any article");
        }

        return articles.Select(_mapper.MapToEntity).ToList();
    }

    public async Task<List<Article>> GetByAuthorId(UserId userId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("UserId", userId.Value);

        var articles = await _connection.QueryAsync<ArticleModel>(
            "SELECT * FROM func_get_articles_by_author_id(@UserId::uuid)",
            parameters,
            commandType: CommandType.Text);

        if (articles is null)
        {
            throw new EntityNotFoundException<NewsModel>("Cannot get any article");
        }

        return articles.Select(_mapper.MapToEntity).ToList();
    }

    public async Task<List<Article>> GetVerifiedByAuthorId(UserId userId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("UserId", userId.Value);

        var articles = await _connection.QueryAsync<ArticleModel>(
            "SELECT * FROM func_get_verified_articles_by_author_id(@UserId::uuid)",
            parameters,
            commandType: CommandType.Text);

        if (articles is null)
        {
            throw new EntityNotFoundException<NewsModel>("Cannot get any article");
        }

        return articles.Select(_mapper.MapToEntity).ToList();
    }

    public async Task<Article?> GetById(ArticleId id)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", id.Value);

        var article = await _connection.QueryFirstOrDefaultAsync<ArticleModel?>(
            "SELECT * FROM func_get_article_by_id(@Id::uuid)",
            parameters,
            commandType: CommandType.Text);

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
        
        var createdArticle = await _connection.QueryFirstOrDefaultAsync<ArticleModel>(
            sql,
            parameters,
            commandType: CommandType.Text);
        
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
        
        var updatedArticle = await _connection.QueryFirstOrDefaultAsync<ArticleModel>(
            sql,
            parameters,
            commandType: CommandType.Text);

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

        var deletedArticleId = await _connection.QueryFirstOrDefaultAsync<Guid>(
            "SELECT * FROM func_delete_article(@Id::uuid)",
            parameters,
            commandType: CommandType.Text);

        if (deletedArticleId == default)
        {
            throw new PersistenceException("Article was not deleted");
        }
            
        return ArticleId.CreateFromGuid(deletedArticleId);
    }
}