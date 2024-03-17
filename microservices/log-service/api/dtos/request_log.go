package dtos

type RequestLog struct {
	Timestamp      string `json:"timestamp"`
	Ip             string `json:"ip"`
	Url            string `json:"url"`
	UserAgent      string `json:"userAgent"`
	RequestString  string `json:"requestString"`
	StatusCode     int    `json:"statusCode"`
	ResponseString string `json:"responseString"`
}
