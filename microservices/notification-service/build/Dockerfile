FROM golang:1.20 AS build
WORKDIR /app
COPY go.mod go.sum ./

RUN go mod download

COPY . .
RUN go build -o /notification_service ./cmd/

FROM alpine:3.23 AS runtime
COPY --from=build /notification_service /notification_service

CMD ["/notification_service"]