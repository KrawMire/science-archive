using ScienceArchive.Core.Domain.Aggregates.News.ValueObjects;
using ScienceArchive.Core.Domain.Common;
using ScienceArchive.Core.Exceptions;

namespace ScienceArchive.Core.Domain.Aggregates.News;

/// <summary>
/// News entity. Represents
/// news of the system
/// </summary>
public class News : AggregateRoot<NewsId>
{
    private string _title;
    private string _body; 
    
    internal News(NewsId? id = null) : base(id ?? NewsId.CreateNew())
    {
    }

    /// <summary>
    /// News title
    /// </summary>
    public required string Title
    {
        get => _title;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidFieldValueException(nameof(Title));
            }

            _title = value;
        }
    }

    /// <summary>
    /// News body
    /// </summary>
    public required string Body
    {
        get => _body;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidFieldValueException(nameof(Body));
            }

            _body = value;
        }
    }

    /// <summary>
    /// News metadata. Contains information about
    /// author, creation and last update date
    /// </summary>
    public required NewsMetadata Metadata { get; set; }
}