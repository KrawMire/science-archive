package communication

import "log"

type TelegramAdapter struct {
}

func NewTelegramAdapter() *TelegramAdapter {
	return &TelegramAdapter{}
}

func (ta *TelegramAdapter) SendMessage(recipient string, message string) error {
	log.Printf("Sent message to %s\n", recipient)
	return nil
}
