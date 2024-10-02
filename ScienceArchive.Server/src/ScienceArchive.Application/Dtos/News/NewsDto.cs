namespace ScienceArchive.Application.Dtos.News;

public record NewsDto
{
    public string? Id { get; set; }
    public required string AuthorId { get; set; }
    public required string Title { get; set; }
    public required string Body { get; set; }
    public DateTime? CreationDate { get; set; }
    public DateTime? LastUpdatedDate { get; set; }
}