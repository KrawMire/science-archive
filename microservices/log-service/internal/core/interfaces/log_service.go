package interfaces

import "log-service/internal/core/models"

type LogService interface {
	LogRequest(log models.RequestLog) error
}
