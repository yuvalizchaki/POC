using Microsoft.AspNetCore.SignalR;
using POC.Contracts.Screen;
using POC.Infrastructure.Extensions;
using POC.Infrastructure.Generators;
using POC.Infrastructure.Repositories;

namespace POC.Api.Hubs;

public class GuestHub(GuestConnectionRepository guestConnectionRepository, ILogger<GuestHub> logger) : Hub
    {
        private static readonly string MsgScreenAdded = "screenAdded";
        private static readonly string MsgPairCode = "pairCode";
        private readonly PairCodeGenerator _pairCodeGenerator = new();
        
        public override async Task OnConnectedAsync()
        {
            var pairingCode = _pairCodeGenerator.GenerateAsync().Result;
            logger.LogInformation("Guest hub connection established");
            await guestConnectionRepository.AddConnectionAsync(pairingCode, Context.ConnectionId);
            await SendMessagePairCode(pairingCode);
            
            await base.OnConnectedAsync();
        }

        
        // The client needs to call to This function from his side when the pairing happened successfully.
        public async Task OnSuccessfulPairing(string pairingCode)
        {
            var connectionId = await guestConnectionRepository.GetConnectionIdByCodeAsync(pairingCode);
            await guestConnectionRepository.RemoveConnectionAsync(pairingCode);
        }

        
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            // var ipAddress = Context.GetHttpContext()?.Connection.RemoteIpAddress?.ToString();
            // if (ipAddress != null)
            // {
            //     await guestConnectionRepository.RemoveConnectionAsync(ipAddress);
            // }

            await base.OnDisconnectedAsync(exception);
        }
        
        
        // public async Task<bool> IsIpConnected(string ipAddress)
        // {
        //     return await guestConnectionRepository.IsConnectionExistsAsync(ipAddress);
        // }

        
        private async Task SendMessageToClient<T>(string pairCode, string method, T message)
        {
            var connectionId = await guestConnectionRepository.GetConnectionIdByCodeAsync(pairCode);
            if (!string.IsNullOrEmpty(connectionId))
            {
                await Clients.Client(connectionId).SendAsync(method, message);
            }
            else
            {
                logger.LogInformation($"[DEBUG] Connection does not exist for paring code: {pairCode}");
            }
        }
        

        public async Task SendMessageAddScreen(string pairCode, ScreenDto screenDto)
        {
            await SendMessageToClient(pairCode,MsgScreenAdded, screenDto);
            await guestConnectionRepository.RemoveConnectionAsync(pairCode);
        }
        
        private async Task SendMessagePairCode(string pairCode)
        {
            await SendMessageToClient(pairCode,MsgPairCode, pairCode);
        }
    }