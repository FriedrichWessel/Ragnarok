package main

import (
	"container/list"
	"fmt"
	"log"
	"net"

	"github.com/voxelbrain/goptions"
)

var (
	clients = list.New()
)

type Client struct {
	ListElement *list.Element
	net.Conn
}

func main() {
	options := struct {
		Listen string        `goptions:"-l, --listen, description='Address to bind HTTP server to'"`
		Help   goptions.Help `goptions:"-h, --help, description='Show this help'"`
	}{
		Listen: "localhost:8080",
	}

	goptions.ParseAndFail(&options)
	log.Printf("Starting server on %s...", options.Listen)
	ln, err := net.Listen("tcp", options.Listen)
	if err != nil {
		log.Fatalf("Could not start server: %s", err)
	}
	for {
		conn, err := ln.Accept()
		if err != nil {
			continue
		}

		client := Client{
			Conn: conn,
		}

		client.ListElement = clients.PushBack(client)
		go handleConnection(client)
	}
}

func handleConnection(c Client) {
	defer c.Conn.Close()
	defer func() {
		clients.Remove(c.ListElement)
	}()
	fmt.Fprintf(c, "Number of clients connected: %d\n", clients.Len())
	buffer := make([]byte, 1024)
	for {
		n, err := c.Read(buffer)
		if err != nil {
			log.Printf("Error reading from client: %s", err)
			return
		}
		log.Printf("Received message from %s: %s", c.RemoteAddr(), string(buffer[0:n]))
		_, err = c.Write(buffer[0:n])
		if err != nil {
			log.Printf("Error writing to client: %s", err)
			return
		}
	}
}
