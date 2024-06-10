using ScienceArchive.Application.Abstractions.Templating;
using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Aggregates.Notification;
using ScienceArchive.Core.Domain.Aggregates.Notification.ValueObjects;
using ScienceArchive.Core.Domain.Events;
using ScienceArchive.Core.Gateways;

namespace ScienceArchive.Application.Events.Handlers;

internal class UserRegisteredEmailEventHandler : IEventHandler<UserRegisteredEvent>
{
    private readonly INotificationGateway _notificationGateway;
    private readonly ITemplateService _templateService;
    
    public UserRegisteredEmailEventHandler(
        INotificationGateway notificationGateway, 
        ITemplateService templateService)
    {
        _notificationGateway = notificationGateway;
        _templateService = templateService;
    }

    public async Task Handle(UserRegisteredEvent eventWrapper, CancellationToken cancellationToken)
    {
        var populatedEmail = await _templateService.GetPopulatedOtpEmailTemplate(eventWrapper.Name, eventWrapper.ConfirmationCode);
        
        var notification = new Notification(NotificationId.CreateNew())
        {
            Message = populatedEmail,
            Receiver = eventWrapper.Email
        };

        await _notificationGateway.SendNotification(notification);
    }
}