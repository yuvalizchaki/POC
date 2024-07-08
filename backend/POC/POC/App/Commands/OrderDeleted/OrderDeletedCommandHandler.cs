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
        
        var wantOrderConnectionIds = interestedScreens
            .Where(screen => screen.ScreenProfile.ScreenProfileFiltering.IsProfileInterestedInOrders())
            .Select(screen => screen.Id)
            .ToList();
        
        await hub.DeleteOrder(wantOrderConnectionIds.ToArray(), request.Id);
        
        var wantInventoryConnectionIds = interestedScreens
            .Where(screen => screen.ScreenProfile.ScreenProfileFiltering.IsProfileInterestedInInventoryItems())
            .Select(screen => screen.Id)
            .ToList();
        
        //todo function that gets the list of screen connected ids and sends them a message to fetch all inventory items again
        //await hub.AddInventoryItems(wantInventoryConnectionIds.ToArray()); 
    }
}