namespace ScienceArchive.Application.Dtos.User;

public record UserDto
{
    public string? Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Login { get; set; }
    public required List<UserArticleDto> Articles { get; set; }
    public string? About { get; set; }
}