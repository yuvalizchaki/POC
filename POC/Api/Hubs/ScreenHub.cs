using POC.Contracts.CrmDTOs;
using POC.Contracts.Screen;

namespace POC.Api.Hubs;
using Microsoft.AspNetCore.SignalR;

public class ScreenHub : Hub
{
    public async Task UpdateOrder(OrderDto orderDto)
    {
        await Clients.All.SendAsync("ReceiveOrderUpdate", orderDto);
    }
    
    public async Task AddOrder(OrderDto orderDto)
    {
        await Clients.All.SendAsync("AddOrder", orderDto);
    }
    
    public async Task DeleteOrder(OrderDto orderDto)
    {
        await Clients.All.SendAsync("DeleteOrder", orderDto);
    }
    
}
