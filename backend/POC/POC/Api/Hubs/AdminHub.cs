using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using POC.Contracts.Screen;
using POC.Infrastructure.Repositories;

namespace POC.Api.Hubs
{
    [Authorize(Roles = "Admin")]
    public class AdminHub : Hub
    {
        private readonly ScreenConnectionRepository _screenConnectionRepository;
        private readonly ILogger<ScreenHub> _logger;
        
        private static readonly string ScreenConnected = "screenConnected";
        private static readonly string ScreenDisconnected = "screenDisconnected";

        public AdminHub(ScreenConnectionRepository screenConnectionRepository, ILogger<ScreenHub> logger)
        {
            _screenConnectionRepository = screenConnectionRepository;
            _logger = logger;
        }

        public async Task NotifyScreenConnected(int screenId)
        {
            await Clients.All.SendAsync(ScreenConnected, screenId);
        }
        
        public async Task NotifyScreenDisonnected(int screenId)
        {
            await Clients.All.SendAsync(ScreenDisconnected, screenId);
        }
        
       
    }
}