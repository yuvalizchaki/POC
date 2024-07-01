using MediatR;
using POC.Api.Hubs;
using POC.Infrastructure.IRepositories;

namespace POC.App.Commands.OrderUpdated;

public class OrderUpdatedCommandHandler(
    ScreenHub hub,
    IOrderRepository repository
    ) : IRequestHandler<OrderUpdatedCommand>
{
    public async Task Handle(OrderUpdatedCommand request, CancellationToken cancellationToken)
    {
        await repository.AddOrUpdateOrderAsync(request.OrderDto);
        
        await hub.UpdateOrder(request.OrderDto);
    }
}