package communication

import (
	"errors"
	"notifications-service/internal/core/models"
)

type TelegramGateway struct {
}

func NewTelegramGateway() *TelegramGateway {
	return &TelegramGateway{}
}

func (ta *TelegramGateway) SendMessage(notification models.Notification) error {
	return errors.New("telegram notifications are not currently supported")
}
