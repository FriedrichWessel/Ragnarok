// Package protocol defines the data structures used to communicate between server
// and clients.
//
// The connection *from* client *to* server uses the following messages:
// - `UserInputMessage`
//
//    Example:
//      {
//        "type": 0
//      }
//
// The connection *from* server *to* client uses the following messages:
// - `GameStateUpdateMessage`
//
//    Example:
//      {
//        "players": [
//      		{
//      			"UUID": "00000000-0000-0000-0000-000000000000",
//      			"model_id": 0,
//      			"position": [0.0, 4.0, 0.0],
//      			"velocity": [1.0, 0.0, 0.0]
//      		},
//      		{ /* ... */ }
//      	],
//      	"map": {
//      		"height": 4.0,
//      		"objects": [
//      			{
//      				"uuid": "00000000-0000-0000-0000-000000000000",
//      				"model_id": 1,
//      				"position": [10.0, 4.0, 0.0]
//      			},
//      			{ /* ... */ }
//      		]
//      	}
//      }
package protocol

type UserInputType uint8

const (
	RUN UserInputType = iota
)

type UserInputMessage struct {
	Typ uint8 `json:"type"`
}

type Vector3 struct {
	X float32 `json:"x"`
	Y float32 `json:"y"`
	Z float32 `json:"z"`
}

type Object struct {
	UUID     string  `json:"uuid"`
	ModelId  uint64  `json:"model_id"`
	Position Vector3 `json:"position"`
}

type Map struct {
	Height  float32  `json:"height"`
	Objects []Object `json:"objects"`
}

type Player struct {
	UUID     string  `json:"uuid"`
	ModelId  uint64  `json:"model_id"`
	Position Vector3 `json:"position"`
	Velocity Vector3 `json:"velocity"`
}

type GameStateUpdateMessage struct {
	players []Player `json:"players"`
	Map     Map      `json:"map"`
}
