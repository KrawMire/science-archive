package models

import "time"

type RequestLog struct {
	Timestamp      time.Time
	Ip             string
	Url            string
	UserAgent      string
	RequestString  string
	StatusCode     int
	ResponseString string
}
