using ScienceArchive.Application.Abstractions.Persistence;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Events;

namespace ScienceArchive.Application.Events.Handlers;

internal class UserRegisteredEventHandler : IEventHandler<UserRegisteredEvent>
{
    private readonly IDbContext _dbContext;

    public UserRegisteredEventHandler(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task Handle(UserRegisteredEvent domainEvent)
    {
        throw new NotImplementedException();
    }
}