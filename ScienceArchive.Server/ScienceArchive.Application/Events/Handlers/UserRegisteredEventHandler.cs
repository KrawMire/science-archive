using ScienceArchive.Application.Abstractions.Persistence;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Events;
using ScienceArchive.Core.Gateways;

namespace ScienceArchive.Application.Events.Handlers;

internal class UserRegisteredEventHandler : IEventHandler<UserRegisteredEvent>
{
    private readonly IDbContext _dbContext;
    private readonly INotificationGateway _notificationGateway;

    public UserRegisteredEventHandler(
        IDbContext dbContext, 
        INotificationGateway notificationGateway)
    {
        _dbContext = dbContext;
        _notificationGateway = notificationGateway;
    }

    public Task Handle(UserRegisteredEvent domainEvent)
    {
        throw new NotImplementedException();
    }
}