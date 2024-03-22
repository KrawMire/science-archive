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
	service interfaces.NotificationService
}

func NewNotificationConsumer(service interfaces.NotificationService) *NotificationConsumer {
	return &NotificationConsumer{
		service: service,
	}
}

func (c *NotificationConsumer) WatchNotificationQueue() {
	conn, err := amqp.Dial("amqp://guest:guest@localhost:5672/")
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
		"notifications",
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

	go c.handleRequestLogMessages(msgs)

	log.Println("Listening to notifications messages...")

	var forever chan struct{}
	<-forever
}

func (c *NotificationConsumer) handleRequestLogMessages(msgs <-chan amqp.Delivery) {
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
			log.Println(err)
			continue
		}

		log.Println("Successfully processed notification")
	}
}
