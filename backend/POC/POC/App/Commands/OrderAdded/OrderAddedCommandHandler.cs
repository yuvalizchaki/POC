using MediatR;
using POC.Api.Hubs;

namespace POC.App.Commands.OrderAdded;

public class OrderAddedCommandHandler (ScreenHub hub) : IRequestHandler<OrderAddedCommand>
{
    public async Task Handle(OrderAddedCommand request, CancellationToken cancellationToken)
    {
        //NOTIFY all screens a new order has been added
        //Todo: remove try-catch block, need to test hub.AddOrder(..) method
        try
        {
            await hub.AddOrder(request.OrderDto);
        }catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }
        
    }
}