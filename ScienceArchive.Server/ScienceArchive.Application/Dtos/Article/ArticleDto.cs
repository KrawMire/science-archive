namespace ScienceArchive.Application.Dtos.Article;

public record ArticleDto
{
    public string? Id { get; set; }
    public required string CategoryId { get; set; }
    public required string CategoryName { get; set; }
    public required string Title { get; set; }
    public required List<ArticleAuthorDto> Authors { get; set; }
    public int Status { get; set; }
    public required List<ArticleDocumentDto> Documents { get; set; }
    public DateTime? CreationDate { get; set; }
    public string? Description { get; set; }
}