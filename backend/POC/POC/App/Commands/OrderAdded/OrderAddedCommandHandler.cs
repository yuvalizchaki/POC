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
        
        var wantOrderScreenIds = interestedScreens
            .Where(screen => screen.ScreenProfile.ScreenProfileFiltering.IsProfileInterestedInOrders())
            .Select(screen => screen.Id)
            .ToList();
        
        await hub.AddOrder(wantOrderScreenIds.ToArray(), request.OrderDto);
        
        var wantInventoryScreenIds = interestedScreens
            .Where(screen => screen.ScreenProfile.ScreenProfileFiltering.IsProfileInterestedInInventoryItems())
            .Select(screen => screen.Id)
            .ToList();
        
        await hub.FetchInventoryItems(wantInventoryScreenIds.ToArray()); 
    }
    
}