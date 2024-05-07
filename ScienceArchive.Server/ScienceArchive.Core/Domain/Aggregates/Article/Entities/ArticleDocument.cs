using ScienceArchive.Core.Domain.Aggregates.Article.ValueObjects;
using ScienceArchive.Core.Domain.Common;

namespace ScienceArchive.Core.Domain.Aggregates.Article.Entities;

public class ArticleDocument : Entity<ArticleDocumentId>
{
	public ArticleDocument(ArticleDocumentId id) : base(id)
	{
	}
	
	/// <summary>
	/// Name of article document
	/// </summary>
	public required string Name { get; set; } 
	
	/// <summary>
	/// Path to a document linked to article
	/// </summary>
	public required string Path { get; set; }
}