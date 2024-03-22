package communication

import "log"

type EmailAdapter struct {
}

func NewEmailAdapter() *EmailAdapter {
	return &EmailAdapter{}
}

func (ta *EmailAdapter) SendMessage(recipient string, message string) error {
	log.Printf("Sent email to %s\n", recipient)
	return nil
}
