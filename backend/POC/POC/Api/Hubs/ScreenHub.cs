using POC.Contracts.CrmDTOs;
using POC.Contracts.Screen;

namespace POC.Api.Hubs;

using Microsoft.AspNetCore.SignalR;

public class ScreenHub : Hub
{
    private static readonly string MsgOrderAdded = "orderAdded";
    private static readonly string MsgOrderUpdated = "orderUpdated";
    private static readonly string MsgOrderDeleted = "orderDeleted";

    public async Task AddOrder(OrderDto orderDto)
    {
        await Clients.All.SendAsync(MsgOrderAdded, orderDto);
    }

    public async Task UpdateOrder(OrderDto orderDto)
    {
        await Clients.All.SendAsync(MsgOrderUpdated, orderDto);
    }

    public async Task DeleteOrder(int orderId)
    {
        await Clients.All.SendAsync(MsgOrderDeleted, orderId);
    }
}