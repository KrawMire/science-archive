using ScienceArchive.Application.Dtos.News;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.News;
using ScienceArchive.Core.Domain.Aggregates.News.Factories;

namespace ScienceArchive.Application.Mappers;

internal class NewsMapper : IApplicationMapper<News, NewsDto>
{
    public NewsDto MapToDto(News entity)
    {
        return new NewsDto
        {
            Id = entity.Id.ToString(),
            Body = entity.Body,
            Title = entity.Title,
            CreationDate = entity.Metadata.CreationDate,
            AuthorId = entity.Metadata.AuthorId.ToString(),
            LastUpdatedDate = entity.Metadata.LastUpdatedDate
        };
    }

    public News MapToEntity(NewsDto model)
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