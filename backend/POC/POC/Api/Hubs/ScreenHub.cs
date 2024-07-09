using Microsoft.AspNetCore.Authorization;
using POC.Contracts.CrmDTOs;
using POC.Contracts.Screen;
using POC.Infrastructure.Repositories;
using Microsoft.AspNetCore.SignalR;


namespace POC.Api.Hubs;

[Authorize(Roles = "Screen")]
public class ScreenHub : Hub
{
    private readonly ScreenConnectionRepository _screenConnectionRepository;
    private readonly ILogger<ScreenHub> _logger;
    private readonly IHubContext<AdminHub> _adminHubContext;

    private static readonly string MsgOrderAdded = "orderAdded";
    private static readonly string MsgOrderUpdated = "orderUpdated";
    private static readonly string MsgOrderDeleted = "orderDeleted";
    private static readonly string ScreenConnected = "screenConnected";
    private static readonly string ScreenDisconnected = "screenDisconnected";
    private static readonly string ProfileUpdated = "profileUpdated";
    private static readonly string MsgInventoryUpdated = "inventoryUpdated";
    
    public ScreenHub(ScreenConnectionRepository screenConnectionRepository, ILogger<ScreenHub> logger, IHubContext<AdminHub> adminHubContext)
    {
        _screenConnectionRepository = screenConnectionRepository;
        _logger = logger;
        _adminHubContext = adminHubContext;
    }

    
    public override async Task OnConnectedAsync()
    {
        var screenId = Context.User.FindFirst("ScreenId");
        if (screenId != null && !string.IsNullOrEmpty(screenId.Value))
        {
            await _screenConnectionRepository.AddConnectionAsync(int.Parse(screenId.Value), Context.ConnectionId);
            _logger.LogInformation($"Screen connected: {screenId}, ConnectionId: {Context.ConnectionId}");
            await _adminHubContext.Clients.All.SendAsync(ScreenConnected, screenId.Value);
        }
        await base.OnConnectedAsync();
    }
    
    
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var screenId = Context.User.FindFirst("ScreenId");
        if (screenId != null && !string.IsNullOrEmpty(screenId.Value))
        {
            await _screenConnectionRepository.RemoveConnectionAsync(int.Parse(screenId.Value));
            _logger.LogInformation($"Screen disconnected: {screenId}, ConnectionId: {Context.ConnectionId}");
            await _adminHubContext.Clients.All.SendAsync(ScreenDisconnected, screenId.Value);
        }
        await base.OnDisconnectedAsync(exception);
    }
    
    
    public async Task NotifyUpdateProfile(int[] screenIds)
    {
        foreach (var id in screenIds)
        {
            var connectionId = _screenConnectionRepository.GetConnectionIdByScreenIdAsync(id);
            if (!string.IsNullOrEmpty(connectionId.Result))
            {
                await Clients.Client(connectionId.Result).SendAsync(ProfileUpdated);
            }
        }
    }

    
    public async Task UpdateOrder(int[] screenIds, OrderDto orderDto)
    {
        foreach (var id in screenIds)
        {
            var connectionId = _screenConnectionRepository.GetConnectionIdByScreenIdAsync(id);
            if (!string.IsNullOrEmpty(connectionId.Result))
            {
                await Clients.Client(connectionId.Result).SendAsync(MsgOrderUpdated, orderDto);
            }
        }
    }
    
    
    public async Task AddOrder(int[] screenIds, OrderDto orderDto)
    {
        foreach (var id in screenIds)
        {
            var connectionId = _screenConnectionRepository.GetConnectionIdByScreenIdAsync(id);
            if (!string.IsNullOrEmpty(connectionId.Result))
            {
                await Clients.Client(connectionId.Result).SendAsync(MsgOrderAdded, orderDto);
            }
        }
    }
    
    
    public async Task DeleteOrder(int[] screenIds, int orderId)
    {
        foreach (var id in screenIds)
        {
            var connectionId = _screenConnectionRepository.GetConnectionIdByScreenIdAsync(id);
            if (!string.IsNullOrEmpty(connectionId.Result))
            {
                await Clients.Client(connectionId.Result).SendAsync(MsgOrderDeleted, orderId);
            }
        } 
    }

    
    public async Task FetchInventoryItems(int[] screenIds)
    {
        foreach (var id in screenIds)
        {
            var connectionId = _screenConnectionRepository.GetConnectionIdByScreenIdAsync(id);
            if (!string.IsNullOrEmpty(connectionId.Result))
            {
                await Clients.Client(connectionId.Result).SendAsync(MsgInventoryUpdated);
            }
        } 
    }
}
