using MediatR;
using POC.Api.Hubs;
using POC.Infrastructure.IRepositories;

namespace POC.App.Commands.OrderAdded;

public class OrderAddedCommandHandler(
    ScreenHub hub,
    IOrderRepository repository
    ) : IRequestHandler<OrderAddedCommand>
{
    public async Task Handle(OrderAddedCommand request, CancellationToken cancellationToken)
    {
        await repository.AddOrUpdateOrderAsync(request.OrderDto);
        
        await hub.AddOrder(request.OrderDto);
    }
}