using POC.Api.Hubs;
using POC.Contracts.CrmDTOs;
using POC.Infrastructure.Adapters;
using POC.Infrastructure.IRepositories;
using POC.Infrastructure.Repositories;

namespace POC.Services;

public interface IOrderService
{
    Task FetchAndReplicateOrdersAsync();
    Task ProcessWebhookOrderAsync(CrmOrder order); 
}

public class OrderService(
    IOrderRepository orderRepository, 
    AdminRepository adminRepository,
    IOrderAdapter orderAdapter, 
    ILogger<OrderService> logger)
    : IOrderService
{
    
    public async Task FetchAndReplicateOrdersAsync()
    {
        try
        {
            var orderDictionary = new Dictionary<int, IEnumerable<CrmOrder>>();
            var adminIds = adminRepository.GetAdminIds();
            foreach (var adminId in adminIds)
            {
                var orders = await orderAdapter.FetchOrdersAsync(adminId);
                orderDictionary[adminId] = orders;
            }
            // Process and save orders
            await orderRepository.SetAllOrdersAsync(orderDictionary);
            
            logger.LogInformation("Orders replicated successfully at {Time}", DateTime.UtcNow);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while replicating orders at {Time}", DateTime.UtcNow);
        }
    }

    public async Task ProcessWebhookOrderAsync(CrmOrder order)
    {
        try
        {
            await orderRepository.AddOrUpdateOrderAsync(order.CompanyId, order);
            logger.LogInformation("Order processed from webhook at {Time}", DateTime.UtcNow);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error processing webhook order at {Time}", DateTime.UtcNow);
        }
    }
}
