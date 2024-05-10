using ScienceArchive.Core.Domain.Aggregates.News;
using ScienceArchive.Core.Domain.Aggregates.News.Factories;
using ScienceArchive.Core.Domain.Aggregates.News.ValueObjects;
using ScienceArchive.Core.Domain.Aggregates.User.ValueObjects;
using ScienceArchive.Infrastructure.Interfaces;
using ScienceArchive.Infrastructure.Persistence.PostgreSql.Models;

namespace ScienceArchive.Infrastructure.PostgreSql.PersistenceMappers;

internal class NewsMapper : IInfrastructureMapper<News, NewsModel>
{
	public NewsModel MapToModel(News entity)
	{
		return new NewsModel
		{
			Id = entity.Id.Value,
			Title = entity.Title,
			Body = entity.Body,
			AuthorId = entity.Metadata.AuthorId.Value,
			CreationDate = entity.Metadata.CreationDate,
			LastUpdatedDate = entity.Metadata.LastUpdatedDate
		};
	}

	public News MapToEntity(NewsModel model)
	{
		var builder = new NewsBuilder(model.Id);

		return builder
			.AddTitle(model.Title)
			.AddBody(model.Body)
			.AddAuthorId(model.AuthorId)
			.AddCreationDate(model.CreationDate)
			.AddLastUpdatedDate(model.LastUpdatedDate)
			.Build();
	}
}