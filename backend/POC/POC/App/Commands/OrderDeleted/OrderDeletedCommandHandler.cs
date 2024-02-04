using MediatR;
using POC.Api.Hubs;
using POC.App.Commands.OrderAdded;

namespace POC.App.Commands.OrderDeleted;

public class OrderDeletedCommandHandler(ScreenHub hub) : IRequestHandler<OrderDeletedCommand>
{
    public async Task Handle(OrderDeletedCommand request, CancellationToken cancellationToken)
    {
        await hub.DeleteOrder(request.Id);
    }
}