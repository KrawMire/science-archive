using System.Data;
using Dapper;
using ScienceArchive.Core.Domain.Aggregates.News;
using ScienceArchive.Core.Domain.Aggregates.News.Repositories;
using ScienceArchive.Core.Domain.Aggregates.News.ValueObjects;
using ScienceArchive.Core.Exceptions;
using ScienceArchive.Infrastructure.Interfaces;
using ScienceArchive.Infrastructure.Persistence.Exceptions;
using ScienceArchive.Infrastructure.Persistence.PostgreSql.Models;

namespace ScienceArchive.Infrastructure.Persistence.PostgreSql.Repositories;

internal class PostgresNewsRepository : INewsRepository
{
    private readonly PostgresExecutionContext _dbContext;
    private readonly IInfrastructureMapper<News, NewsModel> _mapper;

    public PostgresNewsRepository(
        IInfrastructureMapper<News, NewsModel> mapper, 
        PostgresExecutionContext dbContext)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<List<News>> GetAll()
    {
        var news = await _dbContext.Connection.QueryAsync<NewsModel>(
            "SELECT * FROM func_get_all_news()",
            commandType: CommandType.Text,
            transaction: _dbContext.Transaction);

        if (news is null)
        {
            throw new EntityNotFoundException(nameof(News));
        }

        return news.Select(n => _mapper.MapToEntity(n)).ToList();
    }

    public async Task<News?> GetById(NewsId id)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", id.Value);

        var news = await _dbContext.Connection.QueryFirstOrDefaultAsync<NewsModel?>(
            "SELECT * FROM func_get_news_by_id(@Id::uuid)",
            parameters,
            commandType: CommandType.Text,
            transaction: _dbContext.Transaction);

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
        
        var createdNews = await _dbContext.Connection.QueryFirstOrDefaultAsync<NewsModel>(
            sql,
            parameters,
            commandType: CommandType.Text,
            transaction: _dbContext.Transaction);

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

        var deletedNewsId = await _dbContext.Connection.QueryFirstOrDefaultAsync<Guid>(
            "SELECT * FROM func_delete_news(@Id::uuid)",
            parameters,
            commandType: CommandType.Text,
            transaction: _dbContext.Transaction);

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
        
        var updatedNews = await _dbContext.Connection.QueryFirstOrDefaultAsync<NewsModel>(
            sql,
            parameters,
            commandType: CommandType.Text,
            transaction: _dbContext.Transaction);

        if (updatedNews is null)
        {
            throw new PersistenceException("News were not updated!");
        }

        return _mapper.MapToEntity(updatedNews);
    }
}