package application

import (
	"log-service/internal/core/interfaces"
	"log-service/internal/core/models"
)

type LogService struct {
	logRepository interfaces.LogRepository
}

func NewLogService(repository interfaces.LogRepository) *LogService {
	return &LogService{
		logRepository: repository,
	}
}

func (l *LogService) LogRequest(log models.RequestLog) error {
	if err := l.logRepository.CreateRequestLog(log); err != nil {
		return err
	}

	return nil
}
