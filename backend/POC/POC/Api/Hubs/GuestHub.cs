using Microsoft.AspNetCore.SignalR;
using POC.Contracts.Screen;
using POC.Infrastructure.Extensions;
using POC.Infrastructure.Repositories;

namespace POC.Api.Hubs;

public class GuestHub(GuestConnectionRepository guestConnectionRepository, ScreenRepository screenRepository, ILogger<GuestHub> logger) : Hub
    {
        private static readonly string MsgScreenAdded = "screenAdded";
        
        public override async Task OnConnectedAsync()
        {
            logger.LogInformation("Guest hub connection established");
            var ipAddress = Context.GetHttpContext()?.Connection.RemoteIpAddress?.ToString();
            if (ipAddress != null)
            {
                await guestConnectionRepository.AddConnectionAsync(ipAddress, Context.ConnectionId);

                var screens = await screenRepository.GetAllAsync();
                var screen = screens.FirstOrDefault(s => s.IpAddress == ipAddress);
                if (screen != null)
                {
                    var screenDto = screen.ToScreenDto();
                    await SendMessageAddScreen(ipAddress, screenDto);
                }

            }
            else
            {
                logger.LogInformation("[DEBUG] Connection does not exist");
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var ipAddress = Context.GetHttpContext()?.Connection.RemoteIpAddress?.ToString();
            if (ipAddress != null)
            {
                await guestConnectionRepository.RemoveConnectionAsync(ipAddress);
            }

            await base.OnDisconnectedAsync(exception);
        }

        public async Task<bool> IsIpConnected(string ipAddress)
        {
            return await guestConnectionRepository.IsConnectionExistsAsync(ipAddress);
        }

        private async Task SendMessageToIp<T>(string ipAddress, string method, T message)
        {
            var connectionId = await guestConnectionRepository.GetConnectionIdByIpAsync(ipAddress);
            // DEBUG
            // var allConnectionsIps = await guestConnectionRepository.GetAllConnectionIpsAsync();
            // logger.LogInformation($"[DEBUG] All connection ips: {string.Join(", ", allConnectionsIps)}");
            if (!string.IsNullOrEmpty(connectionId))
            {
                await Clients.Client(connectionId).SendAsync(method, message);
            }
            else
            {
                logger.LogInformation($"[DEBUG] Connection does not exist for IP: {ipAddress}");
            }
        }

        public async Task SendMessageAddScreen(string ipAddress, ScreenDto screenDto)
        {
            await SendMessageToIp(ipAddress, MsgScreenAdded, screenDto);
        }
    }