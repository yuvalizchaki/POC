using MediatR;
using POC.Api.Hubs;

namespace POC.App.Commands.OrderAdded;

public class OrderAddedCommandHandler(ScreenHub hub) : IRequestHandler<OrderAddedCommand>
{
    public async Task Handle(OrderAddedCommand request, CancellationToken cancellationToken)
    {
        await hub.AddOrder(request.OrderDto);
    }
}