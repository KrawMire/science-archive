package main

import (
	"log"
	"notifications-service/api"
	"notifications-service/api/consumers"
	"notifications-service/config"
	"notifications-service/internal/application"
	"notifications-service/internal/factories"
)

func main() {
	appConfig := config.GetConfig()

	adapterFactory := factories.NewNotificationAdapterFactory(appConfig.Services)
	service := application.NewNotificationService(adapterFactory)
	consumer := consumers.NewNotificationConsumer(service)
	server := api.NewServer(consumer)

	if err := server.Run(); err != nil {
		log.Fatalf("An error occurred while starting a server: %s", err)
	}
}
