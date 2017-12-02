package main

import (
	"log"
	"net/http"

	"github.com/surma/httptools"
	"github.com/voxelbrain/goptions"
	"golang.org/x/net/websocket"
)

func main() {
	options := struct {
		Listen string        `goptions:"-l, --listen, description='Address to bind HTTP server to'"`
		Help   goptions.Help `goptions:"-h, --help, description='Show this help'"`
	}{
		Listen: "localhost:8080",
	}

	goptions.ParseAndFail(&options)
	app := httptools.List{
		httptools.SilentHandlerFunc(AllowCors),
		httptools.NewRegexpSwitch(map[string]http.Handler{
			"/": http.HandlerFunc(func(w http.ResponseWriter, r *http.Request) {
				w.Write([]byte("<h1>OHAI</h1>"))
			}),
			"/echo": WebsocketEchoServer(),
		}),
	}
	log.Printf("Starting webserver on %s...", options.Listen)
	http.ListenAndServe(options.Listen, app)
}

func AllowCors(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Access-Control-Allow-Origin", "*")
}

func WebsocketEchoServer() websocket.Handler {
	return websocket.Handler(func(c *websocket.Conn) {
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
				log.Printf("Error reading from client: %s", err)
				return
			}
		}
	})
}
