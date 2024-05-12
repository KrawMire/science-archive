using ScienceArchive.Core.Domain.Aggregates.Category.ValueObjects;
using ScienceArchive.Core.Domain.Common;

namespace ScienceArchive.Core.Domain.Aggregates.Article.ValueObjects;

public class ArticleCategory : ValueObject
{
    /// <summary>
    /// ID of category article is referenced to
    /// </summary>
    public required CategoryId CategoryId { get; set; }
    
    /// <summary>
    /// Name of category
    /// </summary>
    public required string Name { get; set; }
}