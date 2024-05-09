namespace ScienceArchive.Infrastructure.Persistence.PostgreSql.Models;

internal record RoleClaimModel
{
	public required string Value { get; set; }
	public string? Description { get; set; }
}