namespace ScienceArchive.Infrastructure.Persistence.PostgreSql.Models;

internal record NewsModel
{
	public required Guid Id { get; set; }
	public required Guid AuthorId { get; set; }
	public required string Title { get; set; }
	public required string Body { get; set; }
	public required DateTime CreationDate { get; set; }
	public required DateTime? LastUpdatedDate { get; set; }
}