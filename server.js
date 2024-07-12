const WebSocket = require('ws')
var uuid = require('uuid-random')

const wss = new WebSocket.WebSocketServer({port:8080}, () => {console.log('server started')})

var playersData = {
	"type": "playerData"
}

wss.on('connection', function connection(client){
	
	//Create unique ID for user
	client.id = uuid();

	console.log('Client ${client.id} Connected!')

	var currentClient = playersData[""+client.id]

	//send default client data for reference
	client.send('{"id": "${client.id}"}')
	
	//Retrieves message from client
	client.on('message', (data) => {
		//parse the data into a JSON data set to make it easier to use
		var dataJSON = JSON.parse(data)

		console.log("Player Message")
		console.log(dataJSON)
	})

	//Notifies when client disconnects
	client.on('close',() => {
		console.log('This Connection Closed!')
		console.log("Removing Client: "+client.id)
	})
})

wss.on("listening", () => {
	console.log('listening on 8080')
})