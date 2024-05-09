namespace ScienceArchive.Infrastructure.Persistence.PostgreSql.Models;

internal record UserModel
{
	public required Guid Id { get; set; }
	public required string Name { get; set; }
	public required string Email { get; set; }
	public required string Login { get; set; }
	public required List<UserArticleModel> Articles { get; set; }
	public string? About { get; set; }
	public List<UserRoleModel>? Roles { get; set; }
	public string? Password { get; set; }
	public string? PasswordSalt { get; set; }
}