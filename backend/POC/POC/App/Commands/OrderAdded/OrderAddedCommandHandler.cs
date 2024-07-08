using MediatR;
using POC.Api.Hubs;
using POC.Contracts.CrmDTOs;
using POC.Infrastructure.Common.utils;
using POC.Infrastructure.Extensions;
using POC.Infrastructure.IRepositories;
using POC.Infrastructure.Models;
using POC.Infrastructure.Repositories;

namespace POC.App.Commands.OrderAdded;

public class OrderAddedCommandHandler(
    ScreenHub hub,
    IOrderRepository orderRepository,
    ScreenConnectionRepository screenConnectionRepository,
    ScreenRepository screenRepository
    ) : IRequestHandler<OrderAddedCommand>
{
    public async Task Handle(OrderAddedCommand request, CancellationToken cancellationToken)
    {
        await orderRepository.AddOrUpdateOrderAsync(request.OrderDto);
        
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
        
        await hub.AddOrder(wantOrderConnectionIds.ToArray(), request.OrderDto);
        
        var wantInventoryConnectionIds = interestedScreens
            .Where(screen => screen.ScreenProfile.ScreenProfileFiltering.IsProfileInterestedInInventoryItems())
            .Select(screen => screen.Id)
            .ToList();
        
        //todo function that gets the list of screen connected ids and sends them a message to fetch all inventory items again
        //await hub.AddInventoryItems(wantInventoryConnectionIds.ToArray()); 
    }
    
}