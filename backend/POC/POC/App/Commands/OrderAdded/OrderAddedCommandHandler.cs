using MediatR;
using POC.Api.Hubs;
using POC.Contracts.CrmDTOs;
using POC.Infrastructure.Common.utils;
using POC.Infrastructure.Extensions;
using POC.Infrastructure.IRepositories;
using POC.Infrastructure.Models;
using POC.Infrastructure.Repositories;
using POC.Services;

namespace POC.App.Commands.OrderAdded;

public class OrderAddedCommandHandler(
    ScreenHub hub,
    IOrderService orderService,
    ScreenConnectionRepository screenConnectionRepository,
    ScreenRepository screenRepository
    ) : IRequestHandler<OrderAddedCommand>
{
    public async Task Handle(OrderAddedCommand request, CancellationToken cancellationToken)
    {
        await orderService.ProcessWebhookOrderAsync(request.CrmOrder);
        
        var connectionIds = await screenConnectionRepository.GetConnectedScreensAsync();
        var screens = await screenRepository.GetScreensByIdsAsync(connectionIds);
        
        var ordersDup = new[] {request.CrmOrder.ToIncomingOrderDto(), request.CrmOrder.ToOutgoingOrderDto()};

        var interestedScreenToOrdersDict = screens
            .ToDictionary(screen => screen, screen =>
                ordersDup.Where(order => screen.ScreenProfile.ScreenProfileFiltering.IsOrderMatch(order)).ToList()
            )
            .Where(pair => pair.Value.Count != 0);
        
        var wantOrderScreesDict = interestedScreenToOrdersDict
            .Where(pair => pair.Key.ScreenProfile.ScreenProfileFiltering.IsProfileInterestedInOrders())
            .Select(pair => KeyValuePair.Create(pair.Key.Id, pair.Value))
            .ToList();
        
        await hub.AddOrder(wantOrderScreesDict);
        
        var wantInventoryScreenIds = interestedScreenToOrdersDict
            .Where(pair => pair.Key.ScreenProfile.ScreenProfileFiltering.IsProfileInterestedInInventoryItems())
            .Select(pair => pair.Key.Id)
            .ToList();
        
        await hub.FetchInventoryItems(wantInventoryScreenIds.ToArray()); 
    }
    
}