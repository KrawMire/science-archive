namespace ScienceArchive.Infrastructure.Persistence.PostgreSql.Models;

internal record ArticleDocumentModel
{
	public required Guid Id { get; set; }
	public required string DocumentName { get; set; }
	public required string DocumentPath { get; set; }
}