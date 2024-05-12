package interfaces

import "log-service/internal/core/models"

type LogRepository interface {
	CreateRequestLog(log models.RequestLog) error
}
