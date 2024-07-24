import WebSocket from 'ws';
import { v4 as uuid } from 'uuid';

const wss = new WebSocket.Server({ port: 8080 }, () => {
  console.log('Server started');
});

const playersData = {}; // Initialize an empty object

wss.on('connection', function connection(client) {
  // Create unique ID for user
  client.id = uuid();

  console.log(`Client ${client.id} Connected!`);

  // Send default client data for reference
  client.send(JSON.stringify({ id: client.id }));

  // Retrieves message from client
  client.on('message', (data) => {
    // Parse the data into a JSON dataset to make it easier to use
    const dataJSON = JSON.parse(data);

    console.log('Player Message');
    console.log(dataJSON);
  });

  // Notifies when client disconnects
  client.on('close', () => {
    console.log('This Connection Closed!');
    console.log(`Removing Client: ${client.id}`);
  });
});

wss.on('listening', () => {
  console.log('Listening on 8080');
});
