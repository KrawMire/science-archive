using ScienceArchive.Core.Domain.Aggregates.Article;
using ScienceArchive.Core.Domain.Aggregates.Article.Entities;
using ScienceArchive.Core.Domain.Aggregates.Article.Enums;
using ScienceArchive.Core.Domain.Aggregates.Article.ValueObjects;
using ScienceArchive.Core.Domain.Aggregates.Category.ValueObjects;
using ScienceArchive.Core.Domain.Aggregates.User.ValueObjects;
using ScienceArchive.Infrastructure.Interfaces;
using ScienceArchive.Infrastructure.Persistence.PostgreSql.Models;

namespace ScienceArchive.Infrastructure.PostgreSql.PersistenceMappers;

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
		var articleId = ArticleId.CreateFromGuid(model.Id);
		
		var authors = model.Authors
			.Select(a => new ArticleAuthor
			{
				UserId = UserId.CreateFromGuid(a.AuthorId),
				Name = a.AuthorName,
				Role = (ArticleAuthorRole)a.Role
			})
			.ToList();
		
		var documents = model.Documents
			.Select(d => new ArticleDocument(ArticleDocumentId.CreateFromGuid(d.Id))
			{
				Name = d.DocumentName, 
				Path = d.DocumentPath
			}).ToList();
		
		return new Article(articleId)
		{
			Title = model.Title,
			Status = (ArticleStatus)model.Status,
			Category = new ArticleCategory
			{
				CategoryId = CategoryId.CreateFromGuid(model.CategoryId),
				Name = model.CategoryName
			},
			Authors = authors,
			Documents = documents,
			CreationDate = model.CreationDate,
			Description = model.Description
		};
	}
}