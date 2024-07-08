using MediatR;
using POC.Api.Hubs;
using POC.Infrastructure.Extensions;
using POC.Infrastructure.IRepositories;
using POC.Infrastructure.Repositories;

namespace POC.App.Commands.OrderUpdated;

public class OrderUpdatedCommandHandler(
    ScreenHub hub,
    IOrderRepository repository,
    ScreenConnectionRepository screenConnectionRepository,
    ScreenRepository screenRepository
    ) : IRequestHandler<OrderUpdatedCommand>
{
    public async Task Handle(OrderUpdatedCommand request, CancellationToken cancellationToken)
    {
        await repository.AddOrUpdateOrderAsync(request.OrderDto);
        
        var connectionIds = await screenConnectionRepository.GetConnectedScreensAsync();
        var screens = await screenRepository.GetScreensByIdsAsync(connectionIds);
        
        var interestedScreens = screens
            .Where(screen => screen.ScreenProfile.ScreenProfileFiltering.IsOrderMatch(request.OrderDto)
            
            )
            .ToList();
        
        var wantOrderConnectionIds = interestedScreens
            .Where(screen => screen.ScreenProfile.ScreenProfileFiltering.IsProfileInterestedInOrders())
            .Select(screen => screen.Id)
            .ToList();
        
        await hub.UpdateOrder(wantOrderConnectionIds.ToArray(), request.OrderDto);
        
        var wantInventoryConnectionIds = interestedScreens
            .Where(screen => screen.ScreenProfile.ScreenProfileFiltering.IsProfileInterestedInInventoryItems())
            .Select(screen => screen.Id)
            .ToList();
        
        //todo function that gets the list of screen connected ids and sends them a message to fetch all inventory items again
        //await hub.AddInventoryItems(wantInventoryConnectionIds.ToArray()); 
    }
}