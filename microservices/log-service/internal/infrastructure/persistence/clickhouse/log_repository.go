package clickhouse

import (
	"context"
	"fmt"
	"github.com/ClickHouse/clickhouse-go/v2"
	"github.com/ClickHouse/clickhouse-go/v2/lib/driver"
	"log"
	"log-service/internal/core/models"
)

type LogRepository struct {
	conn driver.Conn
}

func NewLogRepository(host string, database string, username string, password string) *LogRepository {
	var (
		ctx       = context.Background()
		conn, err = clickhouse.Open(&clickhouse.Options{
			Addr: []string{host},
			Auth: clickhouse.Auth{
				Database: database,
				Username: username,
				Password: password,
			},
			Debugf: func(format string, v ...interface{}) {
				fmt.Printf(format, v)
			},
		})
	)

	if err != nil {
		log.Fatalf("Error while creating clickhouse connection: %s", err)
	}

	if err = conn.Ping(ctx); err != nil {
		log.Fatalf("Exception %s", err)
	}

	return &LogRepository{
		conn: conn,
	}
}

func (l *LogRepository) CreateRequestLog(log models.RequestLog) error {
	query := "insert into request_logs(timestamp, ip, url, user_agent, request_str, response_str) values (?, ?, ?, ?, ?, ?)"

	err := l.conn.AsyncInsert(
		context.Background(),
		query,
		false,
		log.Timestamp,
		log.Ip,
		log.Url,
		log.UserAgent,
		log.RequestString,
		log.ResponseString,
	)

	if err != nil {
		return err
	}

	return nil
}
