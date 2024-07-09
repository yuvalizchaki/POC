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
        
        private static readonly string MsgScreenRemoved = "screenRemoved";

        public AdminHub(ScreenConnectionRepository screenConnectionRepository, ILogger<ScreenHub> logger)
        {
            _screenConnectionRepository = screenConnectionRepository;
            _logger = logger;
        }

        
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveAdminMessage", message);
        }
        
        public async Task RemoveScreen(ScreenDto screen)
        {
            var connectionId = await _screenConnectionRepository.RemoveConnectionAsync(screen.Id);
            _logger.LogInformation($"[DEBUG] connectionId: {connectionId} has been removed");
            if (!string.IsNullOrEmpty(connectionId))
            {
                await Clients.Client(connectionId).SendAsync(MsgScreenRemoved, screen);
            }
        }

        public async Task RemoveScreens(ScreenDto[] screens)
        {
            foreach (var screen in screens)
            {
                await RemoveScreen(screen);
            }
        }
    }
}