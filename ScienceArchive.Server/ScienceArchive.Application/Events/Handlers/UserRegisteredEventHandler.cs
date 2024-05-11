using ScienceArchive.Application.Abstractions.Persistence;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Events;
using ScienceArchive.Core.Gateways;

namespace ScienceArchive.Application.Events.Handlers;

internal class UserRegisteredEventHandler : IEventHandler<UserRegisteredEvent>
{
    private readonly IDbUnitOfWork _dbUnitOfWork;
    private readonly INotificationGateway _notificationGateway;

    public UserRegisteredEventHandler(
        IDbUnitOfWork dbUnitOfWork, 
        INotificationGateway notificationGateway)
    {
        _dbUnitOfWork = dbUnitOfWork;
        _notificationGateway = notificationGateway;
    }

    public Task Handle(UserRegisteredEvent domainEvent)
    {
        throw new NotImplementedException();
    }
}