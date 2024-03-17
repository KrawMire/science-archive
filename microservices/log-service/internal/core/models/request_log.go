package models

type RequestLog struct {
	Timestamp      string
	Ip             string
	Url            string
	UserAgent      string
	RequestString  string
	StatusCode     int
	ResponseString string
}
