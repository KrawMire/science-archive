using System.Reflection;
using ScienceArchive.Application.Abstractions.Persistence;
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
    
    public UserRegisteredEmailEventHandler(
        IDbUnitOfWork dbUnitOfWork, 
        INotificationGateway notificationGateway)
    {
        _dbUnitOfWork = dbUnitOfWork;
        _notificationGateway = notificationGateway;
    }

    public async Task Handle(UserRegisteredEventWrapper eventWrapper, CancellationToken cancellationToken)
    {
        var assemblyPath = Assembly.GetExecutingAssembly().Location;
        var assemblyFolder = Path.GetDirectoryName(assemblyPath)!;
        var htmlTemplateFilePath = Path.Combine(assemblyFolder, "Resources", "EmailTemplates", "otp-code.template.html");
        
        if (!File.Exists(htmlTemplateFilePath))
        {
            throw new FileNotFoundException("Cannot find template file for email");
        }
        
        var emailTemplate = await File.ReadAllTextAsync(htmlTemplateFilePath, cancellationToken);
        
        var notification = new Notification(NotificationId.CreateNew())
        {
            Message = PopulateTemplate(emailTemplate, eventWrapper.Event),
            Receiver = eventWrapper.Event.Email
        };

        await _notificationGateway.SendNotification(notification);
    }

    private string PopulateTemplate(string template, UserRegisteredEvent domainEvent)
    {
        return template
            .Replace("{UserName}", domainEvent.Name)
            .Replace("{ConfirmCode}", domainEvent.ConfirmationCode);
    }
}