using MediatR;
using POC.Api.Hubs;

namespace POC.App.Commands.OrderUpdated;

public class OrderUpdatedCommandHandler(ScreenHub hub) : IRequestHandler<OrderUpdatedCommand>
{
    public async Task Handle(OrderUpdatedCommand request, CancellationToken cancellationToken)
    {
        await hub.UpdateOrder(request.OrderDto);
    }
}