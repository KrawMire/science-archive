package dtos

type NotificationDto struct {
	TargetService int    `json:"targetService"`
	Type          int    `json:"type"`
	MessageTitle  string `json:"messageTitle"`
	Message       string `json:"message"`
	Recipient     string `json:"recipient"`
}
