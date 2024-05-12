using ScienceArchive.Application.Dtos.Article;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.Article;
using ScienceArchive.Core.Domain.Aggregates.Article.Factories;

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
        var builder = new ArticleBuilder(dto.Id);
        
        foreach (var author in dto.Authors)
        {
            builder.AddAuthor(author.UserId, author.Name, author.Role);
        }

        foreach (var document in dto.Documents)
        {
            builder.AddDocument(document.Id, document.Name, document.Path);
        }

        return builder
            .AddCategory(dto.CategoryId, dto.CategoryName)
            .AddTitle(dto.Title)
            .AddStatus(dto.Status)
            .AddCreationDate(dto.CreationDate)
            .AddDescription(dto.Description)
            .Build();
    }
}