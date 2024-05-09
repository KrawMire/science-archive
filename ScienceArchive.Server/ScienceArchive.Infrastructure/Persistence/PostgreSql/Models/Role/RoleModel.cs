namespace ScienceArchive.Infrastructure.Persistence.PostgreSql.Models;

internal record RoleModel
{
	public required Guid Id { get; set; }
	public required string Name { get; set; }
	public required List<RoleClaimModel> Claims { get; set; }
	public string? Description { get; set; }
}