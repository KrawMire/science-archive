package factories

import (
	"errors"
	"notifications-service/internal/core/interfaces"
	"notifications-service/internal/core/models"
	"notifications-service/internal/infrastructure/communication"
)

type NotificationAdapterFactory struct {
}

func NewNotificationAdapterFactory() *NotificationAdapterFactory {
	return &NotificationAdapterFactory{}
}

func (f *NotificationAdapterFactory) NewNotificationAdapter(targetService models.NotificationTargetService) (interfaces.NotificationAdapter, error) {
	switch targetService {
	case models.EMAIL:
		return communication.NewEmailAdapter(), nil
	case models.TELEGRAM:
		return communication.NewTelegramAdapter(), nil
	}

	return nil, errors.New("got invalid target service type")
}
