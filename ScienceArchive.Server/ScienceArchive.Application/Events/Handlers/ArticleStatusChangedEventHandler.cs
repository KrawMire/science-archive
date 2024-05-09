using ScienceArchive.Application.Abstractions.Persistence;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Events;

namespace ScienceArchive.Application.Events.Handlers;

internal class ArticleStatusChangedEventHandler : IEventHandler<ArticleStatusChangedEvent>
{
    private readonly IDbContext _dbContext;

    public ArticleStatusChangedEventHandler(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task Handle(ArticleStatusChangedEvent domainEvent)
    {
        throw new NotImplementedException();
    }
}