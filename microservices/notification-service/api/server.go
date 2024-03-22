package api

import (
	"notifications-service/api/consumers"
)

type Server struct {
	consumer *consumers.NotificationConsumer
}

func NewServer(consumer *consumers.NotificationConsumer) *Server {
	return &Server{
		consumer: consumer,
	}
}

func (s *Server) Run() error {
	s.consumer.WatchNotificationQueue()
	return nil
}
