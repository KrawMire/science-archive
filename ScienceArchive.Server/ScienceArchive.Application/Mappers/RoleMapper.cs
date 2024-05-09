using ScienceArchive.Application.Dtos.Role;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.Role;
using ScienceArchive.Core.Domain.Aggregates.Role.ValueObjects;

namespace ScienceArchive.Application.Mappers;

internal class RoleMapper : IApplicationMapper<Role, RoleDto>
{
    public RoleDto MapToDto(Role entity)
    {
        var claims = entity.Claims
            .Select(c => new RoleClaimDto
            {
                Value = c.Value,
                Description = c.Description
            })
            .ToList();

        return new RoleDto
        {
            Id = entity.Id.ToString(),
            Name = entity.Name,
            Claims = claims,
            Description = entity.Description
        };
    }

    public Role MapToEntity(RoleDto model)
    {
        var claims = model.Claims
            .Select(c => new RoleClaim
            {
                Value = c.Value,
                Description = c.Description
            })
            .ToList();

        var roleId = string.IsNullOrWhiteSpace(model.Id)
            ? null
            : RoleId.CreateNew();
        
        return new Role(roleId)
        {
            Name = model.Name,
            Description = model.Description,
            Claims = claims
        };
    }
}