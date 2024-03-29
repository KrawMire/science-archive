﻿using System.Data;
using Dapper;
using ScienceArchive.Core.Domain.Aggregates.News;
using ScienceArchive.Core.Domain.Aggregates.News.ValueObjects;
using ScienceArchive.Core.Repositories;
using ScienceArchive.Infrastructure.Persistence.Exceptions;
using ScienceArchive.Infrastructure.Persistence.Interfaces;
using ScienceArchive.Infrastructure.Persistence.PostgreSql.Models;

namespace ScienceArchive.Infrastructure.Persistence.PostgreSql.Repositories;

internal class PostgresNewsRepository : INewsRepository
{
    private readonly IDbConnection _connection;
    private readonly IPersistenceMapper<News, NewsModel> _mapper;

    public PostgresNewsRepository(PostgresContext dbContext, IPersistenceMapper<News, NewsModel> mapper)
    {
        var context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _connection = context.CreateConnection();
    }

    public async Task<List<News>> GetAll()
    {
        var news = await _connection.QueryAsync<NewsModel>(
            "SELECT * FROM func_get_all_news()",
            commandType: CommandType.Text);

        if (news is null)
        {
            throw new EntityNotFoundException<NewsModel>("Cannot get any news!");
        }

        return news.Select(n => _mapper.MapToEntity(n)).ToList();
    }

    public async Task<News?> GetById(NewsId id)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", id.Value);

        var news = await _connection.QueryFirstOrDefaultAsync<NewsModel?>(
            "SELECT * FROM func_get_news_by_id(@Id::uuid)",
            parameters,
            commandType: CommandType.Text);

        return news is null ? null : _mapper.MapToEntity(news);
    }

    public async Task<News> Create(News newValue)
    {
        var newsToCreate = _mapper.MapToModel(newValue);
        var parameters = new DynamicParameters(newsToCreate);

        var sql = @"SELECT * FROM func_create_news(
            @Id::uuid, 
            @Title::varchar(255), 
            @Body::text, 
            @AuthorId::uuid, 
            @CreationDate::timestamp)";
        
        var createdNews = await _connection.QueryFirstOrDefaultAsync<NewsModel>(
            sql,
            parameters,
            commandType: CommandType.Text);

        if (createdNews is null)
        {
            throw new PersistenceException("News were not created");
        }

        return _mapper.MapToEntity(createdNews);
    }

    public async Task<NewsId> Delete(NewsId id)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", id.Value);

        var deletedNewsId = await _connection.QueryFirstOrDefaultAsync<Guid>(
            "SELECT * FROM func_delete_news(@Id::uuid)",
            parameters,
            commandType: CommandType.Text);

        if (deletedNewsId == default)
        {
            throw new PersistenceException("News was not deleted");
        }

        return NewsId.CreateFromGuid(deletedNewsId);
    }

    public async Task<News> Update(NewsId id, News newValue)
    {
        var newsToUpdate = _mapper.MapToModel(newValue);
        var parameters = new DynamicParameters(newsToUpdate);
        parameters.Add("Id", id.Value);

        var sql = @"SELECT * FROM func_update_news(
            @Id::uuid, 
            @Title::varchar(255), 
            @Body::text)";
        
        var updatedNews = await _connection.QueryFirstOrDefaultAsync<NewsModel>(
            sql,
            parameters,
            commandType: CommandType.Text);

        if (updatedNews is null)
        {
            throw new PersistenceException("News were not updated!");
        }

        return _mapper.MapToEntity(updatedNews);
    }
}