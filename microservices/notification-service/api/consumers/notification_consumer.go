package consumers

import (
	"encoding/json"
	amqp "github.com/rabbitmq/amqp091-go"
	"log"
	"notifications-service/api/dtos"
	"notifications-service/internal/core/interfaces"
	"notifications-service/internal/core/models"
)

type NotificationConsumer struct {
	connectionString string
	queueName        string
	service          interfaces.NotificationService
}

func NewNotificationConsumer(connectionString string, queueName string, service interfaces.NotificationService) *NotificationConsumer {
	return &NotificationConsumer{
		connectionString: connectionString,
		queueName:        queueName,
		service:          service,
	}
}

func (c *NotificationConsumer) WatchNotificationQueue() {
	conn, err := amqp.Dial(c.connectionString)
	if err != nil {
		log.Fatal("Failed to connect to RabbitMQ")
	}
	defer conn.Close()

	ch, err := conn.Channel()
	if err != nil {
		log.Fatal("Failed to open a channel")
	}
	defer ch.Close()

	q, err := ch.QueueDeclare(
		c.queueName,
		false,
		false,
		false,
		false,
		nil)
	if err != nil {
		log.Fatal("Failed to declare a queue")
	}

	msgs, err := ch.Consume(
		q.Name,
		"",
		true,
		false,
		false,
		false,
		nil)
	if err != nil {
		log.Fatal("Failed to register a consumer")
	}

	go c.handleNotificationsMessages(msgs)

	log.Println("Listening to notifications messages...")

	var forever chan struct{}
	<-forever
}

func (c *NotificationConsumer) handleNotificationsMessages(msgs <-chan amqp.Delivery) {
	for d := range msgs {
		var reqDto dtos.NotificationDto
		err := json.Unmarshal(d.Body, &reqDto)
		if err != nil {
			log.Printf("Cannot unmarshall request DTO: %s\n", err)
			continue
		}

		reqModel := models.Notification{
			TargetService: models.NotificationTargetService(reqDto.TargetService),
			Type:          models.NotificationType(reqDto.Type),
			MessageTitle:  reqDto.MessageTitle,
			Message:       reqDto.Message,
			Recipient:     reqDto.Recipient,
		}

		err = c.service.SendNotification(reqModel)
		if err != nil {
			log.Printf("Error while sending notification: %s\n", err)
			continue
		}

		log.Println("Successfully processed notification")
	}
}
