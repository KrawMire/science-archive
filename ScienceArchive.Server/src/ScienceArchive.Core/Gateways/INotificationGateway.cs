using ScienceArchive.Core.Domain.Aggregates.Notification;

namespace ScienceArchive.Core.Gateways;

/// <summary>
/// Represents a notification gateway for sending notifications.
/// </summary>
public interface INotificationGateway
{
    /// <summary>
    /// Sends a notification using the notification gateway.
    /// </summary>
    /// <param name="notification">The notification to be sent.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task SendNotification(Notification notification);
}