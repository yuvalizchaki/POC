using MediatR;
using POC.Api.Hubs;
using POC.App.Commands.OrderAdded;
using POC.Infrastructure.Extensions;
using POC.Infrastructure.IRepositories;
using POC.Infrastructure.Repositories;

namespace POC.App.Commands.OrderDeleted;

public class OrderDeletedCommandHandler(
    ScreenHub hub,
    IOrderRepository orderRepository,
    ScreenConnectionRepository screenConnectionRepository,
    ScreenRepository screenRepository
    ) : IRequestHandler<OrderDeletedCommand>
{
    public async Task Handle(OrderDeletedCommand request, CancellationToken cancellationToken)
    {
        await orderRepository.DeleteOrderAsync(request.Id);
        
        var connectionIds = await screenConnectionRepository.GetConnectedScreensAsync();
        
        var screens = await screenRepository.GetScreensByIdsAsync(connectionIds);
        
        var order = await orderRepository.GetOrderAsync(request.Id);
        
        var interestedScreens = screens
            .Where(screen => screen.ScreenProfile.ScreenProfileFiltering.IsOrderMatch(order)
            
            )
            .ToList();
        
        var wantOrderScreenIds = interestedScreens
            .Where(screen => screen.ScreenProfile.ScreenProfileFiltering.IsProfileInterestedInOrders())
            .Select(screen => screen.Id)
            .ToList();
        
        await hub.DeleteOrder(wantOrderScreenIds.ToArray(), request.Id);
        
        var wantInventoryScreenIds = interestedScreens
            .Where(screen => screen.ScreenProfile.ScreenProfileFiltering.IsProfileInterestedInInventoryItems())
            .Select(screen => screen.Id)
            .ToList();
        
        await hub.FetchInventoryItems(wantInventoryScreenIds.ToArray()); 
    }
}