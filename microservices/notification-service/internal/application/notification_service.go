package application

import (
	"notifications-service/internal/core/models"
	"notifications-service/internal/factories"
)

type NotificationService struct {
	adapterFactory *factories.NotificationAdapterFactory
}

func NewNotificationService(adapterFactory *factories.NotificationAdapterFactory) *NotificationService {
	return &NotificationService{
		adapterFactory: adapterFactory,
	}
}

func (n *NotificationService) SendNotification(notification models.Notification) error {
	adapter, err := n.adapterFactory.NewNotificationAdapter(notification.TargetService)

	if err != nil {
		return err
	}

	if err = adapter.SendMessage(notification.Recipient, notification.Message); err != nil {
		return err
	}

	return nil
}
