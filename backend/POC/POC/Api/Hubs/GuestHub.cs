using Microsoft.AspNetCore.SignalR;
using POC.Contracts.Screen;
using POC.Infrastructure.Common.Exceptions;
using POC.Infrastructure.Extensions;
using POC.Infrastructure.Generators;
using POC.Infrastructure.IRepositories;
using POC.Infrastructure.Repositories;

namespace POC.Api.Hubs;

public class GuestHub(IGuestConnectionRepository guestConnectionRepository, ILogger<GuestHub> logger) : Hub
    {
        private static readonly string MsgScreenAdded = "screenAdded";
        private static readonly string MsgPairCode = "pairCode";
        private readonly PairCodeGenerator _pairCodeGenerator = new();
        
        public override async Task OnConnectedAsync()
        {
            var pairingCode = guestConnectionRepository.AddConnectionAsync(Context.ConnectionId).Result;
            logger.LogInformation($"Guest hub connection established. Pairing code: {pairingCode}, ConnectionId: {Context.ConnectionId}");
            
            await SendMessagePairCode(pairingCode);
    
            await base.OnConnectedAsync();
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
        
        private async Task SendMessageToClient<T>(string pairCode, string method, T message)
        {
            var connectionId = await guestConnectionRepository.GetConnectionIdByCodeAsync(pairCode, default);
            //var connectionId = Context.ConnectionId;
            if (!string.IsNullOrEmpty(connectionId))
            {
                await Clients.Client(connectionId).SendAsync(method, message);
            }
            else
            {
                logger.LogInformation($"[DEBUG] Connection does not exist for pairing code: {pairCode}");
                throw new PairingCodeDoesNotExistException();
            }
        }
        
        public async Task SendMessageAddScreen(string pairCode, string token)
        {
            await SendMessageToClient(pairCode,MsgScreenAdded, token);
            //await guestConnectionRepository.RemoveConnectionAsync(pairCode);
        }
        
        private async Task SendMessagePairCode(string pairCode)
        {
            await SendMessageToClient(pairCode,MsgPairCode, pairCode);
        }
    }