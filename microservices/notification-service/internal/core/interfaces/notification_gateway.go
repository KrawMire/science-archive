package interfaces

import "notifications-service/internal/core/models"

type NotificationGateway interface {
	SendMessage(notification models.Notification) error
}
