using MediatR;
using POC.Api.Hubs;
using POC.Infrastructure.IRepositories;
using POC.Infrastructure.Repositories;

namespace POC.App.Commands.OrderAdded;

public class OrderAddedCommandHandler(
    ScreenHub hub,
    IOrderRepository repository,
    ScreenConnectionRepository screenConnectionRepository
    ) : IRequestHandler<OrderAddedCommand>
{
    public async Task Handle(OrderAddedCommand request, CancellationToken cancellationToken)
    {
        await repository.AddOrUpdateOrderAsync(request.OrderDto);
        
        var connectionIds = await screenConnectionRepository.GetConnectedScreensAsync();
        
        await hub.AddOrder(connectionIds.ToArray(), request.OrderDto);
    }
}