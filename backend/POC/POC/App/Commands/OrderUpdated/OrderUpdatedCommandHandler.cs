using MediatR;
using POC.Api.Hubs;
using POC.Infrastructure.IRepositories;
using POC.Infrastructure.Repositories;

namespace POC.App.Commands.OrderUpdated;

public class OrderUpdatedCommandHandler(
    ScreenHub hub,
    IOrderRepository repository,
    ScreenConnectionRepository screenConnectionRepository
    ) : IRequestHandler<OrderUpdatedCommand>
{
    public async Task Handle(OrderUpdatedCommand request, CancellationToken cancellationToken)
    {
        await repository.AddOrUpdateOrderAsync(request.OrderDto);
        
        var connectionIds = await screenConnectionRepository.GetConnectedScreensAsync();
        
        await hub.UpdateOrder(connectionIds.ToArray(), request.OrderDto);
    }
}