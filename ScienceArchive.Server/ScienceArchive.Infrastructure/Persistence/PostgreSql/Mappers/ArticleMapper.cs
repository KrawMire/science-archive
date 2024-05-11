using ScienceArchive.Core.Domain.Aggregates.Article;
using ScienceArchive.Core.Domain.Aggregates.Article.Factories;
using ScienceArchive.Infrastructure.Interfaces;
using ScienceArchive.Infrastructure.Persistence.PostgreSql.Models;

namespace ScienceArchive.Infrastructure.Persistence.PostgreSql.Mappers;

internal class ArticleMapper : IInfrastructureMapper<Article, ArticleModel>
{
	public ArticleModel MapToModel(Article entity)
	{
		var authors = entity.Authors
			.Select(a => new ArticleAuthorModel
			{
				AuthorId = a.UserId.Value,
				AuthorName = a.Name,
				Role = (int)a.Role
			}).ToList();
		
		var documents = entity.Documents
			.Select(d => new ArticleDocumentModel
			{
				Id = d.Id.Value,
				DocumentName = d.Name,
				DocumentPath = d.Path
			}).ToList();
		
		return new ArticleModel
		{
			Id = entity.Id.Value,
			CategoryId = entity.Category.CategoryId.Value,
			CategoryName = entity.Category.Name,
			Status = (int)entity.Status,
			Title = entity.Title,
			CreationDate = entity.CreationDate,
			Description = entity.Description,
			Authors = authors,
			Documents = documents,
		};
	}

	public Article MapToEntity(ArticleModel model)
	{
		var builder = new ArticleBuilder(model.Id);
        
		foreach (var author in model.Authors)
		{
			builder.AddAuthor(author.AuthorId, author.AuthorName, author.Role);
		}

		foreach (var document in model.Documents)
		{
			builder.AddDocument(document.Id, document.DocumentName, document.DocumentPath);
		}

		return builder
			.AddCategory(model.CategoryId, model.CategoryName)
			.AddTitle(model.Title)
			.AddStatus(model.Status)
			.AddCreationDate(model.CreationDate)
			.AddDescription(model.Description)
			.Build();
	}
}