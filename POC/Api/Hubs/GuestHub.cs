using Microsoft.AspNetCore.SignalR;
using POC.Infrastructure.Repositories;

namespace POC.Api.Hubs;

public class GuestHub (ConnectionRepository connectionRepository) : Hub
{
    
    public async Task OnConnect()
    {
        var ipAddress = Context.GetHttpContext()?.Connection.RemoteIpAddress?.ToString();
        if (ipAddress != null)
        {
           await connectionRepository.AddConnectionAsync(ipAddress, Context.ConnectionId);
        }
    }
    
    
    public async Task OnDisconnect(string ipAddress)
    {
        await connectionRepository.RemoveConnectionAsync(ipAddress);
    }
    
    
    public async Task<bool> IsIpConnected(string ipAddress)
    {
        return await connectionRepository.IsConnectionExistsAsync(ipAddress);
    }
    
    
    public async Task SendMessageToIp(string ipAddress, string message)
    {
        string connectionId = await connectionRepository.GetConnectionIdAsync(ipAddress);
        if (connectionId != null)
        {
            await Clients.Client(connectionId).SendAsync("ReceiveMessage", message);
        }
        else
        {
            await Clients.Caller.SendAsync("ErrorMessage", "Client with specified IP address is not connected.");
        }
    }

}