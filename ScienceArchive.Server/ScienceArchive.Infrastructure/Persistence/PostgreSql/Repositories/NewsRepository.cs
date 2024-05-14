using Microsoft.EntityFrameworkCore;
using ScienceArchive.Core.Domain.Aggregates.News;
using ScienceArchive.Core.Domain.Aggregates.News.Factories;
using ScienceArchive.Core.Domain.Aggregates.News.Repositories;
using ScienceArchive.Core.Domain.Aggregates.News.ValueObjects;
using ScienceArchive.Core.Exceptions;
using ScienceArchive.Infrastructure.Persistence.Exceptions;

namespace ScienceArchive.Infrastructure.Persistence.PostgreSql.Repositories;

internal class PostgresNewsRepository : INewsRepository
{
    private readonly PostgresDbContext _dbContext;

    public PostgresNewsRepository(PostgresDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<List<News>> GetAll()
    {
        var news = await _dbContext
            .News
            .OrderByDescending(n => n.CreationDate)
            .ToListAsync();

        return news.Select(n =>
            {
                var builder = new NewsBuilder(n.Id);
                
                return builder
                    .AddTitle(n.Title)
                    .AddBody(n.Body)
                    .AddAuthorId(n.AuthorId)
                    .AddCreationDate(n.CreationDate)
                    .AddLastUpdatedDate(n.LastUpdatedDate)
                    .Build();
            }).ToList();
    }

    public async Task<News?> GetById(NewsId id)
    {
        var news = await _dbContext
            .News
            .Where(n => n.Id == id.Value)
            .FirstOrDefaultAsync();

        if (news is null)
        {
            return null;
        }
        
        var builder = new NewsBuilder(news.Id);
            
        return builder
            .AddTitle(news.Title)
            .AddBody(news.Body)
            .AddAuthorId(news.AuthorId)
            .AddCreationDate(news.CreationDate)
            .AddLastUpdatedDate(news.LastUpdatedDate)
            .Build();
    }

    public async Task<News> Create(News newValue)
    {
        var news = await _dbContext
            .News
            .AddAsync(new Entities.News
            {
                Id = newValue.Id.Value,
                AuthorId = newValue.Metadata.AuthorId.Value,
                Title = newValue.Title,
                Body = newValue.Body,
                CreationDate = newValue.Metadata.CreationDate,
                LastUpdatedDate = newValue.Metadata.LastUpdatedDate
            });

        await _dbContext.SaveChangesAsync();

        var createdNews = await GetById(NewsId.CreateFromGuid(news.Entity.Id));

        if (createdNews is null)
        {
            throw new PersistenceException("News were not created");
        }

        return createdNews;
    }

    public async Task<NewsId> Delete(NewsId id)
    {
        var news = await _dbContext
            .News
            .Where(n => n.Id == id.Value)
            .FirstOrDefaultAsync();

        if (news is null)
        {
            throw new EntityNotFoundException(nameof(News));
        }
        
        _dbContext.News.Remove(news);
        await _dbContext.SaveChangesAsync();
        
        return id;
    }

    public async Task<News> Update(NewsId id, News newValue)
    {
        var existingNews = await _dbContext.News.Where(n => n.Id == id.Value).FirstOrDefaultAsync();

        if (existingNews is null)
        {
            throw new EntityNotFoundException(nameof(News));
        }

        existingNews.AuthorId = newValue.Metadata.AuthorId.Value;
        existingNews.Title = newValue.Title;
        existingNews.Body = newValue.Body;
        existingNews.CreationDate = newValue.Metadata.CreationDate;
        existingNews.LastUpdatedDate = newValue.Metadata.LastUpdatedDate;

        await _dbContext.SaveChangesAsync();

        return (await GetById(id))!;
    }
}