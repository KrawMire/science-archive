package clickhouse

import (
	"fmt"
	"log-service/internal/core/models"
)

type LogRepository struct {
}

func NewLogRepository() *LogRepository {
	return &LogRepository{}
}

func (l LogRepository) CreateRequestLog(log models.RequestLog) error {
	fmt.Printf("New Log: %s, %s.", log.Ip, log.Url)
	return nil
}
