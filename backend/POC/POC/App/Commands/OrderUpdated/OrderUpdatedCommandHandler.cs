using MediatR;
using POC.Api.Hubs;

namespace POC.App.Commands.OrderUpdated;

public class OrderUpdatedCommandHandler (ScreenHub hub) : IRequestHandler<OrderUpdatedCommand>
{
    public async Task Handle(OrderUpdatedCommand request, CancellationToken cancellationToken)
    {
        //NOTIFY all screens a new order has been updated
        //Todo: remove try-catch block, need to test hub.UpdateOrder(..) method
        try
        {
            await hub.UpdateOrder(request.OrderDto);
        }catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}