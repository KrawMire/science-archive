package main

import (
	"flag"
	"log"
	"science-archive/doc-store-api/internal/api"
)

func main() {
	listenAddress := flag.String("listenaddr", ":32400", "the server address")
	flag.Parse()

	server := api.NewServer(*listenAddress)
	log.Fatal(server.Start())
}
