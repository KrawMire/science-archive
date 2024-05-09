using ScienceArchive.Application.Dtos.Article;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.Article;
using ScienceArchive.Core.Domain.Aggregates.Article.Entities;
using ScienceArchive.Core.Domain.Aggregates.Article.Enums;
using ScienceArchive.Core.Domain.Aggregates.Article.ValueObjects;
using ScienceArchive.Core.Domain.Aggregates.Category;
using ScienceArchive.Core.Domain.Aggregates.Category.ValueObjects;
using ScienceArchive.Core.Domain.Aggregates.User.ValueObjects;

namespace ScienceArchive.Application.Mappers;

internal class ArticleMapper : IApplicationMapper<Article, ArticleDto>
{
    public ArticleDto MapToDto(Article entity)
    {
        var authors = entity.Authors
            .Select(a => new ArticleAuthorDto
            {
                UserId = a.UserId.ToString(),
                Name = a.Name,
                Role = (int)a.Role
            }).ToList();
        
        var documents = entity.Documents
            .Select(d => new ArticleDocumentDto
            {
                Name = d.Name,
                Path = d.Path
            })
            .ToList();
        
        return new ArticleDto
        {
            Id = entity.Id.ToString(),
            CategoryId = entity.Category.CategoryId.ToString(),
            CategoryName = entity.Category.Name,
            Title = entity.Title,
            Authors = authors,
            Status = (int)entity.Status,
            Documents = documents,
            CreationDate = entity.CreationDate,
            Description = entity.Description
        };
    }

    public Article MapToEntity(ArticleDto dto)
    {
        var articleId = string.IsNullOrWhiteSpace(dto.Id)
            ? null
            : ArticleId.CreateFromString(dto.Id);

        var authors = dto.Authors
            .Select(a => new ArticleAuthor
            {
                UserId = UserId.CreateFromString(a.UserId),
                Name = a.Name,
                Role = (ArticleAuthorRole)a.Role
            })
            .ToList();
        
        var documents = dto.Documents
            .Select(d => new ArticleDocument(null)
            {
                Name = d.Name,
                Path = d.Path
            })
            .ToList();
        
        return new Article(articleId)
        {
            Category = new ArticleCategory
            {
                Name = dto.CategoryName,
                CategoryId = CategoryId.CreateFromString(dto.CategoryId)
            },
            Title = dto.Title,
            Authors = authors,
            Status = (ArticleStatus)dto.Status,
            CreationDate = dto.CreationDate.GetValueOrDefault(DateTime.Now),
            Documents = documents,
            Description = dto.Description
        };
    }
}