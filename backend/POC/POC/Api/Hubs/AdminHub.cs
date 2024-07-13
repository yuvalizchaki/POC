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
        private readonly ILogger<AdminHub> _logger;
        private IHubContext<AdminHub> _context;

        
        private static readonly string ScreenConnected = "screenConnected";
        private static readonly string ScreenDisconnected = "screenDisconnected";
        

        public AdminHub(ILogger<AdminHub> logger, IHubContext<AdminHub> context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task NotifyScreenConnected(int screenId)
        {
            await _context.Clients.All.SendAsync(ScreenConnected, screenId);
        }
        
        public async Task NotifyScreenDisonnected(int screenId)
        {
            await _context.Clients.All.SendAsync(ScreenDisconnected, screenId);
        }
        
       
    }
}