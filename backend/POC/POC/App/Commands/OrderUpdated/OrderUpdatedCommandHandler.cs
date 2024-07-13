using MediatR;
using POC.Api.Hubs;
using POC.Contracts.CrmDTOs;
using POC.Infrastructure.Extensions;
using POC.Infrastructure.IRepositories;
using POC.Infrastructure.Repositories;
using POC.Services;

namespace POC.App.Commands.OrderUpdated;

public class OrderUpdatedCommandHandler(
    ScreenHub hub,
    IOrderService orderService,
    ScreenConnectionRepository screenConnectionRepository,
    ScreenRepository screenRepository
    ) : IRequestHandler<OrderUpdatedCommand>
{
    public async Task Handle(OrderUpdatedCommand request, CancellationToken cancellationToken)
    {
        await orderService.ProcessWebhookOrderAsync(request.CrmOrder);
        
        var connectionIds = await screenConnectionRepository.GetConnectedScreensAsync();
        var screens = await screenRepository.GetScreensByIdsAsync(connectionIds);
        
        var ordersDup = new[] {request.CrmOrder.ToIncomingOrderDto(), request.CrmOrder.ToOutgoingOrderDto()};

        var interestedScreens = screens
            .Where(screen => ordersDup.Any(order => screen.ScreenProfile.ScreenProfileFiltering.IsOrderMatch(order)));

        var wantOrderScreensIds = interestedScreens
            .Where(screen => 
                screen.ScreenProfile.ScreenProfileFiltering.IsProfileInterestedInOrders()
                ||
                screen.ScreenProfile.ScreenProfileFiltering.IsProfileInterestedInInventoryItems())
            .Select(screen => screen.Id)
            .ToList();
        
        await hub.NotifyDataChanged(wantOrderScreensIds);
    }
}