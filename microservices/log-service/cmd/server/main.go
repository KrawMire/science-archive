package main

import (
	"log"
	"log-service/api"
	"log-service/api/consumers"
	"log-service/config"
	"log-service/internal/application"
	"log-service/internal/infrastructure/persistence/clickhouse"
)

func main() {
	appConfig := config.GetConfig()

	logRepository := clickhouse.NewLogRepository(
		appConfig.ClickHouse.Host,
		appConfig.ClickHouse.Database,
		appConfig.ClickHouse.Username,
		appConfig.ClickHouse.Password)
	logService := application.NewLogService(logRepository)
	logConsumer := consumers.NewRequestLogConsumer(
		appConfig.MqConsumer.ConnectionString,
		appConfig.MqConsumer.QueueName,
		logService)

	server := api.NewServer(logConsumer)

	if err := server.Run(); err != nil {
		log.Fatalf("An error occurred while starting a server: %s", err)
	}
}
