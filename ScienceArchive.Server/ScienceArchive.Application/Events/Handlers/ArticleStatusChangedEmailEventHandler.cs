using ScienceArchive.Application.Abstractions.Persistence;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Events;
using ScienceArchive.Core.Gateways;

namespace ScienceArchive.Application.Events.Handlers;

internal class ArticleStatusChangedEmailEventHandler : IEventHandler<ArticleStatusChangedEvent>
{
    private readonly IDbUnitOfWork _dbUnitOfWork;
    private readonly INotificationGateway _notificationGateway;

    public ArticleStatusChangedEmailEventHandler(
        IDbUnitOfWork dbUnitOfWork, 
        INotificationGateway notificationGateway)
    {
        _dbUnitOfWork = dbUnitOfWork;
        _notificationGateway = notificationGateway;
    }

    public Task Handle(ArticleStatusChangedEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Article #{notification.ArticleId} status was changed to {notification.Status.ToString()}");
        // TODO: Complete later
        return Task.CompletedTask;
    }
}