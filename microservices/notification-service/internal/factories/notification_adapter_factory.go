package factories

import (
	"errors"
	"notifications-service/config"
	"notifications-service/internal/core/interfaces"
	"notifications-service/internal/core/models"
	"notifications-service/internal/infrastructure/communication"
)

type NotificationAdapterFactory struct {
	config config.ServicesConfig
}

func NewNotificationAdapterFactory(notifyConfig config.ServicesConfig) *NotificationAdapterFactory {
	return &NotificationAdapterFactory{
		config: notifyConfig,
	}
}

func (f *NotificationAdapterFactory) NewNotificationAdapter(targetService models.NotificationTargetService) (interfaces.NotificationGateway, error) {
	switch targetService {
	case models.EMAIL:
		emailConfig := &f.config.Email
		return communication.NewEmailGateway(
			emailConfig.Sender,
			emailConfig.Login,
			emailConfig.Password,
			emailConfig.Host,
			emailConfig.Port), nil
	case models.TELEGRAM:
		return communication.NewTelegramGateway(), nil
	}

	return nil, errors.New("got invalid target service type")
}
