using ScienceArchive.Core.Domain.Aggregates.Role;
using ScienceArchive.Core.Domain.Aggregates.Role.ValueObjects;
using ScienceArchive.Infrastructure.Interfaces;
using ScienceArchive.Infrastructure.Persistence.PostgreSql.Models;

namespace ScienceArchive.Infrastructure.PostgreSql.PersistenceMappers;

internal class RoleMapper : IInfrastructureMapper<Role, RoleModel>
{
	public RoleModel MapToModel(Role entity)
	{
		var claims = entity.Claims
			.Select(c => new RoleClaimModel
			{
				Value = c.Value,
				Description = c.Description
			}).ToList(); 
			
		return new RoleModel
		{
			Id = entity.Id.Value,
			Name = entity.Name,
			Description = entity.Description,
			Claims = claims
		};
	}

	public Role MapToEntity(RoleModel model)
	{
		var roleId = RoleId.CreateFromGuid(model.Id);
		var claims = model.Claims
			.Select(c => new RoleClaim
			{
				Value = c.Value,
				Description = c.Description
			}).ToList();

		return new Role(roleId)
		{
			Name = model.Name,
			Description = model.Description,
			Claims = claims
		};
	}
}