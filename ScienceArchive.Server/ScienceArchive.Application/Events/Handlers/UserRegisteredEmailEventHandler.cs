using ScienceArchive.Application.Abstractions.Persistence;
using ScienceArchive.Application.Abstractions.Templating;
using ScienceArchive.Application.Events.EventWrappers;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.Notification;
using ScienceArchive.Core.Domain.Aggregates.Notification.ValueObjects;
using ScienceArchive.Core.Domain.Events;
using ScienceArchive.Core.Gateways;

namespace ScienceArchive.Application.Events.Handlers;

internal class UserRegisteredEmailEventHandler : IEventHandler<UserRegisteredEventWrapper, UserRegisteredEvent>
{
    private readonly IDbUnitOfWork _dbUnitOfWork;
    private readonly INotificationGateway _notificationGateway;
    private readonly ITemplateService _templateService;
    
    public UserRegisteredEmailEventHandler(
        IDbUnitOfWork dbUnitOfWork, 
        INotificationGateway notificationGateway, 
        ITemplateService templateService)
    {
        _dbUnitOfWork = dbUnitOfWork;
        _notificationGateway = notificationGateway;
        _templateService = templateService;
    }

    public async Task Handle(UserRegisteredEventWrapper eventWrapper, CancellationToken cancellationToken)
    {
        var populatedEmail = await _templateService.GetPopulatedOtpEmailTemplate(eventWrapper.Event.Name, eventWrapper.Event.ConfirmationCode);
        
        var notification = new Notification(NotificationId.CreateNew())
        {
            Message = populatedEmail,
            Receiver = eventWrapper.Event.Email
        };

        await _notificationGateway.SendNotification(notification);
    }
}