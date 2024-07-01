using POC.Contracts.CrmDTOs;
using POC.Infrastructure.Adapters;
using POC.Infrastructure.IRepositories;

namespace POC.Infrastructure.Services;

public interface IOrderService
{
    Task FetchAndReplicateOrdersAsync();
    Task ProcessWebhookOrderAsync(OrderDto order); 
}

public class OrderService(IOrderRepository orderRepository, IOrderAdapter orderAdapter, ILogger<OrderService> logger)
    : IOrderService
{
    
    public async Task FetchAndReplicateOrdersAsync()
    {
        try
        {
            // TODO change the companyId accordingly in the future
            var orders = await orderAdapter.FetchOrdersAsync();

            // Process and save orders
            await orderRepository.SetAllOrdersAsync(orders);

            logger.LogInformation("Orders replicated successfully at {Time}", DateTime.UtcNow);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while replicating orders at {Time}", DateTime.UtcNow);
        }
    }

    public async Task ProcessWebhookOrderAsync(OrderDto order)
    {
        try
        {
            await orderRepository.AddOrUpdateOrderAsync(order);
            logger.LogInformation("Order processed from webhook at {Time}", DateTime.UtcNow);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error processing webhook order at {Time}", DateTime.UtcNow);
        }
    }
}
