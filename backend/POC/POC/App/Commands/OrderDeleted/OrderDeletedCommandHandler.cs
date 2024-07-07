using MediatR;
using POC.Api.Hubs;
using POC.App.Commands.OrderAdded;
using POC.Infrastructure.Extensions;
using POC.Infrastructure.IRepositories;
using POC.Infrastructure.Repositories;

namespace POC.App.Commands.OrderDeleted;

public class OrderDeletedCommandHandler(
    ScreenHub hub,
    IOrderRepository repository,
    ScreenConnectionRepository screenConnectionRepository,
    ScreenRepository screenRepository,
    IOrderRepository orderRepository
    ) : IRequestHandler<OrderDeletedCommand>
{
    public async Task Handle(OrderDeletedCommand request, CancellationToken cancellationToken)
    {
        await repository.DeleteOrderAsync(request.Id);
        
        var connectionIds = await screenConnectionRepository.GetConnectedScreensAsync();
        
        var screens = await screenRepository.GetScreensByIdsAsync(connectionIds);
        
        var order = await orderRepository.GetOrderAsync(request.Id);
        
        connectionIds = screens
            .Where(screen => screen.ScreenProfile.ScreenProfileFiltering.IsOrderMatch(order))
            .Select(screen => screen.Id)
            .ToList();
        
        await hub.DeleteOrder(connectionIds.ToArray(), request.Id);
        //TODO change this to tell the screen to get the data again
        //to make sure its always consistent with what we have in the backend
        //same in add,update
    }
}