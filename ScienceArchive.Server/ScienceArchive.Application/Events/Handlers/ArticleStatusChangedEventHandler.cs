using ScienceArchive.Application.Abstractions.Persistence;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Events;
using ScienceArchive.Core.Gateways;

namespace ScienceArchive.Application.Events.Handlers;

internal class ArticleStatusChangedEventHandler : IEventHandler<ArticleStatusChangedEvent>
{
    private readonly IDbContext _dbContext;
    private readonly INotificationGateway _notificationGateway;

    public ArticleStatusChangedEventHandler(
        IDbContext dbContext, 
        INotificationGateway notificationGateway)
    {
        _dbContext = dbContext;
        _notificationGateway = notificationGateway;
    }

    public Task Handle(ArticleStatusChangedEvent domainEvent)
    {
        throw new NotImplementedException();
    }
}