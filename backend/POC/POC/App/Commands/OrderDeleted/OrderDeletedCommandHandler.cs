using MediatR;
using POC.Api.Hubs;
using POC.App.Commands.OrderAdded;
using POC.Infrastructure.IRepositories;

namespace POC.App.Commands.OrderDeleted;

public class OrderDeletedCommandHandler(
    ScreenHub hub,
    IOrderRepository repository
    ) : IRequestHandler<OrderDeletedCommand>
{
    public async Task Handle(OrderDeletedCommand request, CancellationToken cancellationToken)
    {
        await repository.DeleteOrderAsync(request.Id);
        
        await hub.DeleteOrder(request.Id);
        //TODO change this to tell the screen to get the data again
        //to make sure its always consistent with what we have in the backend
        //same in add,update
    }
}