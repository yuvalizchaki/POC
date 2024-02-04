using MediatR;
using POC.Api.Hubs;
using POC.App.Commands.OrderAdded;

namespace POC.App.Commands.OrderDeleted;

public class OrderDeletedCommandHandler (ScreenHub hub) : IRequestHandler<OrderDeletedCommand>
{
    public async Task Handle(OrderDeletedCommand request, CancellationToken cancellationToken)
    {
        //NOTIFY all screens a new order has been deleted
        //Todo: remove try-catch block, need to test hub.DeleteOrder(..) method
        try
        {
            await hub.DeleteOrder(request.OrderDto);
        }catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }
        
    }
}