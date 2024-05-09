using System.Data;
using Dapper;
using ScienceArchive.Core.Domain.Aggregates.Role;
using ScienceArchive.Core.Domain.Aggregates.Role.Repositories;
using ScienceArchive.Core.Domain.Aggregates.Role.ValueObjects;
using ScienceArchive.Core.Domain.Aggregates.User.ValueObjects;
using ScienceArchive.Core.Exceptions;
using ScienceArchive.Infrastructure.Interfaces;
using ScienceArchive.Infrastructure.Persistence.Exceptions;
using ScienceArchive.Infrastructure.Persistence.PostgreSql.Models;

namespace ScienceArchive.Infrastructure.Persistence.PostgreSql.Repositories;

internal class PostgresRoleRepository : IRoleRepository
{
    private readonly PostgresExecutionContext _dbContext;
    private readonly IInfrastructureMapper<Role, RoleModel> _roleMapper;
    private readonly IInfrastructureMapper<RoleClaim, RoleClaimModel> _claimMapper;

    public PostgresRoleRepository(
        IInfrastructureMapper<Role, RoleModel> roleMapper,
        IInfrastructureMapper<RoleClaim, RoleClaimModel> claimMapper, 
        PostgresExecutionContext dbContext)
    {
        _claimMapper = claimMapper ?? throw new ArgumentNullException(nameof(claimMapper));
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _roleMapper = roleMapper ?? throw new ArgumentNullException(nameof(roleMapper));
    }

    /// <inheritdoc/>
    public async Task<List<Role>> GetAll()
    {
        var roles = await _dbContext.Connection.QueryAsync<RoleModel>(
            "SELECT * FROM func_get_all_roles()",
            commandType: CommandType.Text,
            transaction: _dbContext.Transaction);

        if (roles is null)
        {
            throw new EntityNotFoundException(nameof(Role));
        }

        return roles.Select(role => _roleMapper.MapToEntity(role)).ToList();
    }

    /// <inheritdoc/>
    public async Task<Role?> GetById(RoleId id)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", id.Value);

        var role = await _dbContext.Connection.QuerySingleOrDefaultAsync<RoleModel?>(
            "SELECT * FROM func_get_role_by_id(@Id::uuid)",
            parameters,
            commandType: CommandType.Text,
            transaction: _dbContext.Transaction);

        return role is null ? null : _roleMapper.MapToEntity(role);
    }
    
    /// <inheritdoc/>
    public async Task<List<RoleClaim>> GetUserClaims(UserId userId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("UserId", userId.Value);

        var claims = await _dbContext.Connection.QueryAsync<RoleClaimModel>(
            "SELECT * FROM func_get_claims_by_user_id(@UserId::uuid)",
            parameters,
            commandType: CommandType.Text,
            transaction: _dbContext.Transaction);

        if (claims is null)
        {
            throw new EntityNotFoundException(nameof(RoleClaim));
        }

        return claims
            .Select(_claimMapper.MapToEntity)
            .ToList();
    }

    /// <inheritdoc/>
    public async Task<Role> Create(Role newValue)
    {
        var roleToCreate = _roleMapper.MapToModel(newValue);
        var parameters = new DynamicParameters(roleToCreate);

        var sql = @"SELECT * FROM func_create_role(
            @Id::uuid, 
            @Name::varchar(255), 
            @Description::varchar(255), 
            @ClaimsIds::uuid[])";
        
        var createdRole = await _dbContext.Connection.QuerySingleOrDefaultAsync<RoleModel>(
            sql,
            parameters,
            commandType: CommandType.Text,
            transaction: _dbContext.Transaction);

        if (createdRole is null)
        {
            throw new PersistenceException("Role was not created!");
        }

        return _roleMapper.MapToEntity(createdRole);
    }

    /// <inheritdoc/>
    public async Task<RoleId> Delete(RoleId id)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", id.Value);

        var deletedRoleId = await _dbContext.Connection.QuerySingleOrDefaultAsync<Guid>(
            "SELECT * FROM func_delete_role(@Id::uuid)",
            parameters,
            commandType: CommandType.Text,
            transaction: _dbContext.Transaction);

        if (deletedRoleId == default)
        {
            throw new PersistenceException("Role was not deleted!");
        }

        return RoleId.CreateFromGuid(deletedRoleId);
    }

    /// <inheritdoc/>
    public async Task<Role> Update(RoleId id, Role newValue)
    {
        var roleToUpdate = _roleMapper.MapToModel(newValue);
        var parameters = new DynamicParameters(roleToUpdate);
        parameters.Add("Id", id.Value);

        var sql = @"SELECT * FROM func_update_role(
            @Id::uuid,
            @Name::varchar(255), 
            @Description::varchar(255), 
            @ClaimsIds::uuid[])";
        
        var updatedRole = await _dbContext.Connection.QuerySingleOrDefaultAsync<RoleModel>(
            sql,
            parameters,
            commandType: CommandType.Text,
            transaction: _dbContext.Transaction);

        if (updatedRole is null)
        {
            throw new PersistenceException("Role was not updated!");
        }

        return _roleMapper.MapToEntity(updatedRole);
    }
}