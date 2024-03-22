package interfaces

type NotificationAdapter interface {
	SendMessage(recipient string, message string) error
}
