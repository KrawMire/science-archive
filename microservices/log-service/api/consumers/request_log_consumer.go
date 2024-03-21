package consumers

import (
	"encoding/json"
	amqp "github.com/rabbitmq/amqp091-go"
	"log"
	"log-service/api/dtos"
	"log-service/internal/core/interfaces"
	"log-service/internal/core/models"
)

type RequestLogConsumer struct {
	connectionString string
	queueName        string
	service          interfaces.LogService
}

func NewRequestLogConsumer(connectionString string, queueName string, service interfaces.LogService) *RequestLogConsumer {
	return &RequestLogConsumer{
		connectionString: connectionString,
		queueName:        queueName,
		service:          service,
	}
}

func (c *RequestLogConsumer) WatchRequestLogsQueue() {
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

	go c.handleRequestLogMessages(msgs)

	log.Println("Listening to request logs messages...")

	var forever chan struct{}
	<-forever
}

func (c *RequestLogConsumer) handleRequestLogMessages(msgs <-chan amqp.Delivery) {
	for d := range msgs {
		var reqDto dtos.RequestLogDto
		err := json.Unmarshal(d.Body, &reqDto)
		if err != nil {
			log.Printf("Cannot unmarshall request DTO: %s\n", err)
			continue
		}

		reqModel := models.RequestLog{
			Timestamp:      reqDto.Timestamp,
			Ip:             reqDto.Ip,
			Url:            reqDto.Url,
			UserAgent:      reqDto.UserAgent,
			RequestString:  reqDto.RequestString,
			StatusCode:     reqDto.StatusCode,
			ResponseString: reqDto.ResponseString,
		}

		err = c.service.LogRequest(reqModel)
		if err != nil {
			log.Println(err)
			continue
		}

		log.Println("Successfully processed request log message")
	}
}
