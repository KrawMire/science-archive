package api

import (
	"log-service/api/consumers"
)

type Server struct {
	consumer *consumers.RequestLogConsumer
}

func NewServer(consumer *consumers.RequestLogConsumer) *Server {
	return &Server{
		consumer: consumer,
	}
}

func (s *Server) Run() error {
	s.consumer.WatchRequestLogsQueue()
	return nil
}
