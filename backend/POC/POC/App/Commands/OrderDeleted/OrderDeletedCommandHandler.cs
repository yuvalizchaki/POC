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
        
        var crmOrder = await orderRepository.GetOrderAsync(request.Id);
        
        var ordersDup = new[] {crmOrder.ToIncomingOrderDto(),crmOrder.ToOutgoingOrderDto()};

        var interestedScreenToOrdersDict = screens
            .ToDictionary(screen => screen, screen =>
                ordersDup.Where(order => screen.ScreenProfile.ScreenProfileFiltering.IsOrderMatch(order)).ToList()
            )
            .Where(pair => pair.Value.Count != 0);
        
        
        var wantOrderScreesDict = interestedScreenToOrdersDict
            .Where(pair => pair.Key.ScreenProfile.ScreenProfileFiltering.IsProfileInterestedInOrders())
            .Select(pair => KeyValuePair.Create(pair.Key.Id, pair.Value.Select(order=>order.CrmOrder.Id).ToArray()))
            .ToList();
        
        await hub.DeleteOrder(wantOrderScreesDict);
        
        var wantInventoryScreenIds = interestedScreenToOrdersDict
            .Where(pair => pair.Key.ScreenProfile.ScreenProfileFiltering.IsProfileInterestedInInventoryItems())
            .Select(pair => pair.Key.Id)
            .ToList();
        
        await hub.FetchInventoryItems(wantInventoryScreenIds.ToArray()); 
    }
}