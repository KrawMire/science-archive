package models

type NotificationTargetService int
type NotificationType int

const (
	EMAIL NotificationTargetService = iota
	TELEGRAM
)

const (
	BLOG NotificationType = iota
)

type Notification struct {
	TargetService NotificationTargetService
	Type          NotificationType
	MessageTitle  string
	Message       string
	Recipient     string
}
