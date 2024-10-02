using System.Net.WebSockets;
using System.Text;

Console.WriteLine("Connecting to WebSocket Server...");

using (ClientWebSocket ws = new ClientWebSocket())
{
    await ws.ConnectAsync(new Uri("ws://localhost:5000"), CancellationToken.None);
    Console.WriteLine("Connected!");

    // Sending a message to the WebSocket server
    var message = "Hello, WebSocket Server!";
    var bytes = Encoding.UTF8.GetBytes(message);
    await ws.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None);
    Console.WriteLine($"Sent: {message}");

    // Receiving echo from the WebSocket server
    var buffer = new byte[1024];
    var result = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
    var receivedMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);
    Console.WriteLine($"Received: {receivedMessage}");

    // Close the WebSocket connection
    await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
    Console.WriteLine("Connection closed.");
}
