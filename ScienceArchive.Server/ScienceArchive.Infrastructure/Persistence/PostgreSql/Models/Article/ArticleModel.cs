namespace ScienceArchive.Infrastructure.Persistence.PostgreSql.Models;

internal record ArticleModel
{
	public required Guid Id { get; set; }
	public required Guid CategoryId { get; set; }
	public required string CategoryName { get; set; }
	public required string Title { get; set; }
	public required List<ArticleAuthorModel> Authors { get; set; }
	public required int Status { get; set; }
	public required DateTime CreationDate { get; set; }
	public required List<ArticleDocumentModel> Documents { get; set; }
	public string? Description { get; set; }
}