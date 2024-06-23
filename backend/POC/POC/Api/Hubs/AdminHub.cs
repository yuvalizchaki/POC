using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace POC.Api.Hubs
{
    [Authorize(Roles = "Admin")]
    public class AdminHub : Hub
    {
        public async Task NotifyScreenStatus(string message)
        {
            await Clients.All.SendAsync("ReceiveScreenStatus", message);
        }
    }
}