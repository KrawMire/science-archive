FROM golang:1.20 AS build
WORKDIR /app
COPY go.mod go.sum ./

RUN go mod download

COPY . .
ENV CGO_ENABLED=0
RUN go build -o /log_service ./cmd/server

FROM alpine:3.23 AS runtime
COPY --from=build /log_service /log_service

CMD ["/log_service"]