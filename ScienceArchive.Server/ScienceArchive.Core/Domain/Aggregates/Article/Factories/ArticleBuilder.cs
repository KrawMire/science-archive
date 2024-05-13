using ScienceArchive.Core.Domain.Aggregates.Article.Entities;
using ScienceArchive.Core.Domain.Aggregates.Article.Enums;
using ScienceArchive.Core.Domain.Aggregates.Article.ValueObjects;
using ScienceArchive.Core.Domain.Aggregates.Category.ValueObjects;
using ScienceArchive.Core.Domain.Aggregates.User.ValueObjects;
using ScienceArchive.Core.Exceptions;

namespace ScienceArchive.Core.Domain.Aggregates.Article.Factories;

public class ArticleBuilder
{
    private ArticleId _id;
    private string _title = string.Empty;
    private ArticleStatus _status = ArticleStatus.ToVerify;
    private DateTime _creationDate = DateTime.Now;
    private List<ArticleDocument> _documents = new();
    private List<ArticleAuthor> _authors = new();
    
    private ArticleCategory? _category;
    private string? _description;

    public ArticleBuilder() : this(ArticleId.CreateNew()) {}
    
    public ArticleBuilder(string? articleId)
    {
        _id = articleId is not null 
            ? ArticleId.CreateFromString(articleId) 
            : ArticleId.CreateNew();
    }

    public ArticleBuilder(Guid? articleId)
    {
        _id = articleId is not null 
            ? ArticleId.CreateFromGuid((Guid)articleId) 
            : ArticleId.CreateNew();
    }

    public ArticleBuilder(ArticleId? articleId)
    {
        _id = articleId ?? ArticleId.CreateNew();
    }

    public ArticleBuilder AddTitle(string title)
    {
        _title = title;
        return this;
    }

    public ArticleBuilder AddStatus(int statusNum)
    {
        if (!Enum.IsDefined(typeof(ArticleStatus), statusNum))
        {
            throw new InvalidFieldValueException("Status");
        }
        
        return AddStatus((ArticleStatus)statusNum);
    }
    
    public ArticleBuilder AddStatus(ArticleStatus status)
    {
        _status = status;
        return this;
    }

    public ArticleBuilder AddCategory(string categoryId, string categoryName)
    {
        return AddCategory(SubcategoryId.CreateFromString(categoryId), categoryName);
    }
    
    public ArticleBuilder AddCategory(Guid categoryId, string categoryName)
    {
        return AddCategory(SubcategoryId.CreateFromGuid(categoryId), categoryName);
    }

    public ArticleBuilder AddCategory(SubcategoryId categoryId, string categoryName)
    {
        _category = new ArticleCategory
        {
            CategoryId = categoryId,
            Name = categoryName
        };

        return this;
    }

    public ArticleBuilder AddAuthor(string userId, string name, int roleNum)
    {
        if (!Enum.IsDefined(typeof(ArticleAuthorRole), roleNum))
        {
            throw new InvalidFieldValueException("AuthorRole");
        }
        
        return AddAuthor(UserId.CreateFromString(userId), name, (ArticleAuthorRole)roleNum);
    }
    
    public ArticleBuilder AddAuthor(Guid userId, string name, int roleNum)
    {
        if (!Enum.IsDefined(typeof(ArticleAuthorRole), roleNum))
        {
            throw new InvalidFieldValueException("AuthorRole");
        }
        
        return AddAuthor(UserId.CreateFromGuid(userId), name, (ArticleAuthorRole)roleNum);
    }
    
    public ArticleBuilder AddAuthor(UserId userId, string name, ArticleAuthorRole roleNum)
    {
        _authors.Add(new ArticleAuthor
        {
            UserId = userId,
            Name = name,
            Role = roleNum
        });

        return this;
    }
    
    public ArticleBuilder AddDocument(string? documentId, string name, string path)
    {
        var docId = documentId is not null
            ? ArticleDocumentId.CreateFromString(documentId)
            : ArticleDocumentId.CreateNew();
        
        return AddDocument(docId, name, path);
    }
    
    public ArticleBuilder AddDocument(Guid? documentId, string name, string path)
    {
        var docId = documentId is not null
            ? ArticleDocumentId.CreateFromGuid((Guid)documentId)
            : ArticleDocumentId.CreateNew();
        
        return AddDocument(docId, name, path);
    }
    
    public ArticleBuilder AddDocument(ArticleDocumentId? documentId, string name, string path)
    {
        documentId ??= ArticleDocumentId.CreateNew();
        
        _documents.Add(new ArticleDocument(documentId)
        {
            Name = name,
            Path = path
        });

        return this;
    }
    
    public ArticleBuilder AddCreationDate(DateTime? creationDate)
    {
        _creationDate = creationDate ?? DateTime.Now;
        return this;
    }
    
    public ArticleBuilder AddDescription(string? description)
    {
        _description = description;
        return this;
    }

    public Article Build()
    {
        if (_category is null)
        {
            throw new InvalidFieldValueException("Category");
        }
        
        return new Article(_id)
        {
            Title = _title,
            Status = _status,
            Category = _category,
            Authors = _authors,
            CreationDate = _creationDate,
            Documents = _documents,
            Description = _description
        };
    }
}