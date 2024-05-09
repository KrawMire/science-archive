using System.Data;
using Dapper;
using ScienceArchive.Core.Domain.Aggregates.User;
using ScienceArchive.Core.Domain.Aggregates.User.Repositories;
using ScienceArchive.Core.Domain.Aggregates.User.ValueObjects;
using ScienceArchive.Core.Exceptions;
using ScienceArchive.Infrastructure.Interfaces;
using ScienceArchive.Infrastructure.Persistence.Exceptions;
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
    public async Task<User?> GetById(UserId id)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", id.Value);

        var user = await _dbContext.Connection.QuerySingleOrDefaultAsync<UserModel?>(
            "SELECT * FROM func_get_user_by_id(@Id::uuid)", 
            parameters, 
            commandType: CommandType.Text,
            transaction: _dbContext.Transaction);

        return user is null ? null : _userMapper.MapToEntity(user);
    }

    /// <inheritdoc/>
    public async Task<List<User>> GetAll()
    {
        var users = await _dbContext.Connection.QueryAsync<UserModel>(
            "SELECT * FROM func_get_all_users()", 
            commandType: CommandType.Text,
            transaction: _dbContext.Transaction);

        if (users is null)
        {
            throw new EntityNotFoundException(nameof(User));
        }

        return users.Select(user => _userMapper.MapToEntity(user)).ToList();
    }

    /// <inheritdoc/>
    public async Task<User> Create(User newUser)
    {
        var userToCreate = _userMapper.MapToModel(newUser);
        var parameters = new DynamicParameters(userToCreate);

        var sql = @"SELECT * FROM func_create_user(
            @Id::uuid, 
            @Name::varchar(100), 
            @Email::varchar(50), 
            @Login::varchar(30), 
            @Password::varchar(255), 
            @PasswordSalt::varchar(255), 
            @RolesIds::uuid[])";
        
        var createdUser = await _dbContext.Connection.QuerySingleOrDefaultAsync<UserModel>(
            sql,
            parameters,
            commandType: CommandType.Text,
            transaction: _dbContext.Transaction);

        if (createdUser is null)
        {
            throw new PersistenceException("New user was not created!");
        }

        return _userMapper.MapToEntity(createdUser);
    }

    /// <inheritdoc/>
    public async Task<User> Update(UserId id, User newUser)
    {
        var userToUpdate = _userMapper.MapToModel(newUser);
        var parameters = new DynamicParameters(userToUpdate);
        parameters.Add("Id", id.Value);

        var sql = @"SELECT * FROM func_update_user(
            @Id::uuid, 
            @Name::varchar(100), 
            @Email::varchar(50), 
            @Login::varchar(30), 
            @Password::varchar(255), 
            @PasswordSalt::varchar(255), 
            @RolesIds::uuid[])";
        
        var updatedUser = await _dbContext.Connection.QuerySingleOrDefaultAsync<UserModel>(
            sql,
            parameters,
            commandType: CommandType.Text,
            transaction: _dbContext.Transaction);

        if (updatedUser is null)
        {
            throw new PersistenceException("New user was not updated!");
        }

        return _userMapper.MapToEntity(updatedUser);
    }

    /// <inheritdoc/>
    public async Task<UserId> Delete(UserId id)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", id.Value);

        var deletedUserId = await _dbContext.Connection.QuerySingleOrDefaultAsync<Guid>(
            "SELECT * FROM func_delete_user(@Id::uuid)",
            parameters,
            commandType: CommandType.Text,
            transaction: _dbContext.Transaction);

        if (deletedUserId == default)
        {
            throw new PersistenceException("User was not deleted!");
        }

        return UserId.CreateFromGuid(deletedUserId);
    }
}