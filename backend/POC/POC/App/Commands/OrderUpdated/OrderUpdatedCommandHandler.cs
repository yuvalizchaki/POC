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
        
        connectionIds = screens
            .Where(screen => screen.ScreenProfile.ScreenProfileFiltering.IsOrderMatch(request.OrderDto))
            .Select(screen => screen.Id)
            .ToList();
        
        await hub.UpdateOrder(connectionIds.ToArray(), request.OrderDto);
    }
}