using MediatR;
using Microsoft.AspNetCore.Authorization;
using POC.Contracts.CrmDTOs;
using POC.Infrastructure.Repositories;
using Microsoft.AspNetCore.SignalR;
using POC.App.Commands.NotifyScreenConnected;
using POC.App.Commands.NotifyScreenDisonnected;
using POC.App.Commands.OrderAdded;
using POC.Contracts.Screen;
using POC.Infrastructure.Common.Notifiers;
using POC.Infrastructure.IRepositories;


namespace POC.Api.Hubs;

[Authorize(Roles = "Screen")]
public class ScreenHub : Hub, NotifyOnOrdersChanged
{
    private readonly ScreenConnectionRepository _screenConnectionRepository;
    private readonly ILogger<ScreenHub> _logger;
    // private readonly IHubContext<AdminHub> _adminHubContext;
    private readonly IMediator _mediator;

    private static readonly string MsgRefreshData = "refreshData";
    private static readonly string MsgOrderAdded = "orderAdded";
    private static readonly string MsgOrderUpdated = "orderUpdated";
    private static readonly string MsgOrderDeleted = "orderDeleted";
    private static readonly string ProfileUpdated = "profileUpdated";
    private static readonly string MsgInventoryUpdated = "inventoryUpdated";
    private static readonly string MsgScreenRemoved = "screenRemoved";
    
    public ScreenHub(
        ScreenConnectionRepository screenConnectionRepository, 
        ILogger<ScreenHub> logger, 
        IOrderRepository orderRepository,
        IMediator mediator
       )
    {
        _screenConnectionRepository = screenConnectionRepository;
        _logger = logger;
        orderRepository.SetNotifyOnOrdersChanged(this);
        _mediator = mediator;
    }

    
    public override async Task OnConnectedAsync()
    {
        var screenId = Context.User.FindFirst("ScreenId");
        if (screenId != null && !string.IsNullOrEmpty(screenId.Value))
        {
            await _screenConnectionRepository.AddConnectionAsync(int.Parse(screenId.Value), Context.ConnectionId);
            _logger.LogInformation($"Screen connected: {screenId}, ConnectionId: {Context.ConnectionId}");
            var command = new NotifyScreenConnectedCommand(int.Parse(screenId.Value));
            await _mediator.Send(command);
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
            var command = new NotifyScreenDisconnectedCommand(int.Parse(screenId.Value));
            await _mediator.Send(command);
        }
        await base.OnDisconnectedAsync(exception);
    }
    
    public async Task NotifyDataRefreshed()
    {
        await Clients.All.SendAsync(MsgRefreshData);
        _logger.LogInformation($"notifies all screens to refresh data");
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
    
    public async Task UpdateOrder(List<KeyValuePair<int, List<OrderDto>>> screenToOrders)
    {
        foreach (var pair in screenToOrders)
        {
            var connectionId = _screenConnectionRepository.GetConnectionIdByScreenIdAsync(pair.Key);
            if (!string.IsNullOrEmpty(connectionId.Result))
            {
                await Clients.Client(connectionId.Result).SendAsync(MsgOrderUpdated, pair.Value);
            }
        }
    }
    
    
    public async Task AddOrder(List<KeyValuePair<int, List<OrderDto>>> screenToOrders)
    {
        foreach (var pair in screenToOrders)
        {
            var connectionId = _screenConnectionRepository.GetConnectionIdByScreenIdAsync(pair.Key);
            if (!string.IsNullOrEmpty(connectionId.Result))
            {
                await Clients.Client(connectionId.Result).SendAsync(MsgOrderAdded, pair.Value);
            }
        }
    }
    
    public async Task DeleteOrder(List<KeyValuePair<int, int[]>> screenToOrderIds)
    {
        foreach (var pair in screenToOrderIds)
        {
            var connectionId = _screenConnectionRepository.GetConnectionIdByScreenIdAsync(pair.Key);
            if (!string.IsNullOrEmpty(connectionId.Result))
            {
                await Clients.Client(connectionId.Result).SendAsync(MsgOrderDeleted, pair.Value);
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

    public Task NotifyAsync()
    {
        return NotifyDataRefreshed();
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
