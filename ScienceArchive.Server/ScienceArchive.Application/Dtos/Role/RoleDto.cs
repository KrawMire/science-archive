namespace ScienceArchive.Application.Dtos.Role;

public record RoleDto
{
    public string? Id { get; set; }
    public required string Name { get; set; }
    public required List<RoleClaimDto> Claims { get; init; }
    public string? Description { get; set; }
}