using ScienceArchive.Core.Domain.Aggregates.User;
using ScienceArchive.Core.Domain.Aggregates.User.Repositories;
using ScienceArchive.Core.Domain.Aggregates.User.ValueObjects;
using ScienceArchive.Infrastructure.Interfaces;
using ScienceArchive.Infrastructure.Persistence.PostgreSql.Models;

namespace ScienceArchive.Infrastructure.Persistence.PostgreSql.Repositories;

internal class PostgresUserRepository : IUserRepository
{
    private readonly PostgresExecutionContext _dbContext;
    private readonly IInfrastructureMapper<User, UserModel> _userMapper;

    public PostgresUserRepository(
        IInfrastructureMapper<User, UserModel> userMapper, 
        PostgresExecutionContext dbContext)
    {
        _userMapper = userMapper ?? throw new ArgumentNullException(nameof(userMapper));
        _dbContext = dbContext;
    }

    /// <inheritdoc/>
    public Task<User?> GetById(UserId id)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<List<User>> GetAll()
    {
        throw new NotImplementedException();
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