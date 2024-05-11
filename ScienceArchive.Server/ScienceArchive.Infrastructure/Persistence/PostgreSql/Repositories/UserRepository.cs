using Microsoft.EntityFrameworkCore;
using ScienceArchive.Core.Domain.Aggregates.User;
using ScienceArchive.Core.Domain.Aggregates.User.Factories;
using ScienceArchive.Core.Domain.Aggregates.User.Repositories;
using ScienceArchive.Core.Domain.Aggregates.User.ValueObjects;

namespace ScienceArchive.Infrastructure.Persistence.PostgreSql.Repositories;

internal class PostgresUserRepository : IUserRepository
{
    private readonly PostgresDbContext _dbContext;

    public PostgresUserRepository(PostgresDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <inheritdoc/>
    public async Task<User?> GetById(UserId id)
    {
        var user = await _dbContext.Users
            .Where(u => u.Id == id.Value)
            .FirstOrDefaultAsync();

        if (user is null)
        {
            return null;
        }
            
        var builder = new UserBuilder(user.Id);
                
        return builder
            .AddEmail(user.Email)
            .AddLogin(user.Login)
            .AddName(user.Name)
            .AddAboutText(user.About)
            .Build();
    }

    /// <inheritdoc/>
    public Task<User?> GetUserByLoginOrEmail(string login)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<User?> GetUserByLogin(string login)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<User?> GetUserByEmail(string email)
    {
        throw new NotImplementedException();
    }
    
    /// <inheritdoc/>
    public async Task<List<User>> GetAll()
    {
        var users = await _dbContext
            .Users
            .Include(u => u.UsersArticles)
            .ThenInclude(a => a.Article)
            .ToListAsync();

        return users.Select(u =>
            {
                var builder = new UserBuilder(u.Id);
                
                foreach (var userArticle in u.UsersArticles)
                {
                    builder.AddArticle(userArticle.ArticleId, userArticle.Article.Title);
                }
                
                return builder
                    .AddEmail(u.Email)
                    .AddLogin(u.Login)
                    .AddName(u.Name)
                    .AddAboutText(u.About)
                    .Build();
            })
            .ToList();
    }

    /// <inheritdoc/>
    public Task<User> Create(User newUser)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<User> Update(UserId id, User newUser)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<UserId> Delete(UserId id)
    {
        throw new NotImplementedException();
    }
}