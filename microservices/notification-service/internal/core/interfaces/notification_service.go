package interfaces

import "notifications-service/internal/core/models"

type NotificationService interface {
	SendNotification(notification models.Notification) error
}
