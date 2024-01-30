namespace POC.Api.Hubs;
using Microsoft.AspNetCore.SignalR;

public class ScreenHub : Hub
{
    // Tal: not gonna take credit, I have no idea on how to implement this with MediatR this will probably
    //      be the last thing we implement.  
    
    // // Example method to send a message to all connected clients
    // public async Task SendMessageToAll(string message)
    // {
    //     await Clients.All.SendAsync("ReceiveMessage", message);
    // }
    //
    // // More methods to interact with clients
    // // For example, updating screen data, handling connections, etc.
    //
    // // You can override Hub lifecycle methods if needed
    // public override async Task OnConnectedAsync()
    // {
    //     // Custom logic when a client connects
    //     await base.OnConnectedAsync();
    // }
    //
    // public override async Task OnDisconnectedAsync(Exception exception)
    // {
    //     // Custom logic when a client disconnects
    //     await base.OnDisconnectedAsync(exception);
    // }
}
