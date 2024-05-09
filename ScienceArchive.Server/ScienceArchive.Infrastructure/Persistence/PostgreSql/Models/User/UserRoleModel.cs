namespace ScienceArchive.Infrastructure.Persistence.PostgreSql.Models;

public record UserRoleModel
{
    public required Guid RoleId { get; set; }
}