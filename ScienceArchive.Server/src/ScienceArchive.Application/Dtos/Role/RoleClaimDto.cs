namespace ScienceArchive.Application.Dtos.Role;

public record RoleClaimDto
{
    public required string Value { get; set; }
    public string? Description { get; set; }
}