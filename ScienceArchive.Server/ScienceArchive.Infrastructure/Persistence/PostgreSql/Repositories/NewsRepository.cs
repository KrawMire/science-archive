using ScienceArchive.Core.Domain.Aggregates.News;
using ScienceArchive.Core.Domain.Aggregates.News.Repositories;
using ScienceArchive.Core.Domain.Aggregates.News.ValueObjects;

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
        throw new NotImplementedException();
    }

    public async Task<News?> GetById(NewsId id)
    {
        throw new NotImplementedException();
    }

    public async Task<News> Create(News newValue)
    {
        throw new NotImplementedException();
    }

    public async Task<NewsId> Delete(NewsId id)
    {
        throw new NotImplementedException();
    }

    public async Task<News> Update(NewsId id, News newValue)
    {
        throw new NotImplementedException();
    }
}