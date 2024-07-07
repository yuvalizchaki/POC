using MediatR;
using POC.Api.Hubs;
using POC.Infrastructure.Extensions;
using POC.Infrastructure.IRepositories;
using POC.Infrastructure.Repositories;

namespace POC.App.Commands.OrderAdded;

public class OrderAddedCommandHandler(
    ScreenHub hub,
    IOrderRepository repository,
    ScreenConnectionRepository screenConnectionRepository,
    ScreenRepository screenRepository
    ) : IRequestHandler<OrderAddedCommand>
{
    public async Task Handle(OrderAddedCommand request, CancellationToken cancellationToken)
    {
        await repository.AddOrUpdateOrderAsync(request.OrderDto);
        
        var connectionIds = await screenConnectionRepository.GetConnectedScreensAsync();
        
        var screens = await screenRepository.GetScreensByIdsAsync(connectionIds);
        
        connectionIds = screens
            .Where(screen => screen.ScreenProfile.ScreenProfileFiltering.IsOrderMatch(request.OrderDto))
            .Select(screen => screen.Id)
            .ToList();
        
        await hub.AddOrder(connectionIds.ToArray(), request.OrderDto);
    }
}