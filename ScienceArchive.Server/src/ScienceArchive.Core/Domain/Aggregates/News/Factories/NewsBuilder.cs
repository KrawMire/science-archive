using ScienceArchive.Core.Domain.Aggregates.News.ValueObjects;
using ScienceArchive.Core.Domain.Aggregates.User.ValueObjects;
using ScienceArchive.Core.Exceptions;

namespace ScienceArchive.Core.Domain.Aggregates.News.Factories;

public class NewsBuilder
{
    private NewsId _id;
    private string _title = string.Empty;
    private string _body = string.Empty;
    private DateTime _creationDate = DateTime.Now;
    private DateTime? _lastUpdatedDate;
    
    private UserId? _authorId;

    public NewsBuilder() : this(NewsId.CreateNew()) {}
    
    public NewsBuilder(string? newsId)
    {
        _id = newsId is not null 
            ? NewsId.CreateFromString(newsId) 
            : NewsId.CreateNew();
    }

    public NewsBuilder(Guid? newsId)
    {
        _id = newsId is not null 
            ? NewsId.CreateFromGuid((Guid)newsId) 
            : NewsId.CreateNew();
    }

    public NewsBuilder(NewsId? newsId)
    {
        _id = newsId ?? NewsId.CreateNew();
    }

    public NewsBuilder AddTitle(string title)
    {
        _title = title;
        return this;
    }

    public NewsBuilder AddBody(string body)
    {
        _body = body;
        return this;
    }

    public NewsBuilder AddAuthorId(string authorId)
    {
        return AddAuthorId(UserId.CreateFromString(authorId));
    }
    
    public NewsBuilder AddAuthorId(Guid authorId)
    {
        return AddAuthorId(UserId.CreateFromGuid(authorId));
    }
    
    public NewsBuilder AddAuthorId(UserId authorId)
    {
        _authorId = authorId;
        return this;
    }

    public NewsBuilder AddCreationDate(DateTime? creationDate)
    {
        if (creationDate is not null)
        {
            _creationDate = (DateTime)creationDate;   
        }
        
        return this;
    }

    public NewsBuilder AddLastUpdatedDate(DateTime? lastUpdatedDate)
    {
        _lastUpdatedDate = lastUpdatedDate;
        return this;
    }

    public News Build()
    {
        if (_authorId is null)
        {
            throw new InvalidFieldValueException("AuthorId");
        }
        
        return new News(_id)
        {
            Title = _title,
            Body = _body,
            Metadata = new NewsMetadata
            {
                AuthorId = _authorId,
                CreationDate = _creationDate,
                LastUpdatedDate = _lastUpdatedDate
            }
        };
    }
}