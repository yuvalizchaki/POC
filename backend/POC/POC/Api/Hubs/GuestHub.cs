using System.Text.Json;
using Microsoft.AspNetCore.SignalR;
using POC.Contracts.Screen;
using POC.Infrastructure.Repositories;

namespace POC.Api.Hubs;

public class GuestHub(ConnectionRepository connectionRepository, ILogger<GuestHub> logger) : Hub
{
    private static readonly string ScreenAdded = "screenAdded";

    public async Task OnConnect()
    {
        if (Context != null)
        {
            logger.LogInformation($"Guest hub connection established");
            var ipAddress = Context.GetHttpContext()?.Connection.RemoteIpAddress?.ToString();
            if (ipAddress != null)
            {
                await connectionRepository.AddConnectionAsync(ipAddress, Context.ConnectionId);
            }
        }
        else
        {
            logger.LogInformation($"[DEBUG] Connection does not exist");
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


    private async Task SendMessageToIp<T>(string ipAddress, string method, T message)
    {
        if (Context != null)
        {
            await Clients.Client(Context.ConnectionId).SendAsync(method, message);
        }
        else
        {
            logger.LogInformation($"[DEBUG] Connection does not exist");
        }
        // var connectionId = await connectionRepository.GetConnectionIdAsync(ipAddress);
        // if (connectionId != null)
        // {
        //     logger.LogInformation($"[DEBUG] (SendMessageToIp) connectionId: " + connectionId);
        //     await Clients.Client(connectionId).SendAsync(method, message);
        // }
        // else
        // {
        //     logger.LogInformation($"[DEBUG] (SendMessageToIp) connectionId: NULL");
        //     await Clients.Caller.SendAsync("ErrorMessage", "Client with specified IP address is not connected.");
        // }
    }

    public async Task SendMessageAddScreen(string ipAddress, ScreenDto screenDto)
    {
        await SendMessageToIp(ipAddress, ScreenAdded, screenDto);
    }
}