using POC.Contracts.CrmDTOs;
using POC.Contracts.Screen;
using POC.Infrastructure.Repositories;
using Microsoft.AspNetCore.SignalR;

namespace POC.Api.Hubs;

public class ScreenHub : Hub
{
    private readonly ScreenConnectionRepository _screenConnectionRepository;
    private readonly ILogger<ScreenHub> _logger;

    private static readonly string MsgOrderAdded = "orderAdded";
    private static readonly string MsgOrderUpdated = "orderUpdated";
    private static readonly string MsgOrderDeleted = "orderDeleted";
    private static readonly string MsgScreenRemoved = "screenRemoved";

    public ScreenHub(ScreenConnectionRepository screenConnectionRepository, ILogger<ScreenHub> logger)
    {
        _screenConnectionRepository = screenConnectionRepository;
        _logger = logger;
    }

    //public override async Task OnConnectedAsync()
    //{
    //    var screenId = GetScreenIdFromJwtToken(Context.User); // TODO: Implement
    //    if (!string.IsNullOrEmpty(screenId))
    //    {
    //        await _screenConnectionRepository.AddConnectionAsync(screenId, Context.ConnectionId);
    //        _logger.LogInformation($"Screen connected: {screenId}, ConnectionId: {Context.ConnectionId}");
    //    }
    //    await base.OnConnectedAsync();
    //}

    //public override async Task OnDisconnectedAsync(Exception? exception)
    //{
    //    var screenId = Context.GetHttpContext().Request.Query["screenId"];
    //    if (!string.IsNullOrEmpty(screenId))
    //    {
    //        await _screenConnectionRepository.RemoveConnectionAsync(screenId);
    //        _logger.LogInformation($"Screen disconnected: {screenId}, ConnectionId: {Context.ConnectionId}");
    //    }
    //    await base.OnDisconnectedAsync(exception);
    //}


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

    //public async Task RemoveScreen(ScreenDto screen)
    //{
    //    var connectionId = await _screenConnectionRepository.GetConnectionIdByScreenIdAsync(screen.Id);
    //    _logger.LogInformation($"[DEBUG] connectionId: {connectionId}");
    //    if (!string.IsNullOrEmpty(connectionId))
    //    {
    //        await Clients.Client(connectionId).SendAsync(MsgScreenRemoved, screen);
    //    }
    //}

    //public async Task RemoveScreens(int[] screenIds)
    //{
    //    var connectionIds = await _screenConnectionRepository.GetConnectionIdsByScreenIdsAsync(screenIds);
    //    foreach (var connectionId in connectionIds)
    //    {
    //        await Clients.Client(connectionId).SendAsync(MsgScreenRemoved, screenIds);
    //    }
    //}

}
