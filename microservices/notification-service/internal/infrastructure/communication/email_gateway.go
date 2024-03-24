package communication

import (
	"fmt"
	"log"
	"net/smtp"
	"notifications-service/internal/core/models"
)

type EmailGateway struct {
	sender   string
	login    string
	password string
	host     string
	port     string
}

func NewEmailGateway(
	sender string,
	login string,
	password string,
	host string,
	port string) *EmailGateway {
	return &EmailGateway{
		sender:   sender,
		login:    login,
		password: password,
		host:     host,
		port:     port,
	}
}

func (eg *EmailGateway) SendMessage(notification models.Notification) error {
	smtpFullHost := eg.host + ":" + eg.port

	message := eg.prepareEmail(notification.MessageTitle, notification.Message)
	messageBytes := []byte(message)

	auth := smtp.PlainAuth(
		"",
		eg.login,
		eg.password,
		eg.host)

	err := smtp.SendMail(
		smtpFullHost,
		auth,
		eg.login,
		[]string{notification.Recipient},
		messageBytes)
	if err != nil {
		return err
	}

	log.Println("Email sent successfully!")
	return nil
}

func (eg *EmailGateway) prepareEmail(subject string, message string) string {
	from := eg.sender + "<" + eg.login + ">"
	res := fmt.Sprintf("From: %s \r\nSubject: %s \r\n%s", from, subject, message)

	return res
}
