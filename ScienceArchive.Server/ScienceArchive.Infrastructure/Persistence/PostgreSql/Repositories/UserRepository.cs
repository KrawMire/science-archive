using ScienceArchive.Core.Domain.Aggregates.User;
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
    public Task<User?> GetById(UserId id)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public async Task<List<User>> GetAll()
    {
        throw new NotImplementedException();
        // var users = await _dbContext.Users.ToListAsync();
        // return users.Select(_userMapper.MapToEntity).ToList();
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

    public Task<User?> GetUserByLoginOrEmail(string login)
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetUserByLogin(string login)
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetUserByEmail(string email)
    {
        throw new NotImplementedException();
    }
}