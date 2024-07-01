using Microsoft.Extensions.Caching.Memory;
using POC.Contracts.CrmDTOs;
using POC.Infrastructure.IRepositories;


namespace POC.Infrastructure.Repositories;

public class InMemoryOrderRepository(
    IMemoryCache cache,
    ILogger<InMemoryOrderRepository> logger
    ) : IOrderRepository
{

    private readonly string _cacheKey = "Orders";
    
    public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync(int companyId)
    {
        return await GetAllOrdersFromCache(companyId);
    }

    public Task SetAllOrdersAsync(IEnumerable<OrderDto> orders)
    {
        cache.Set(_cacheKey, orders, TimeSpan.FromMinutes(15));
        return Task.CompletedTask;
    }
    
    public async Task AddOrUpdateOrderAsync(OrderDto order)
    {
        var orders = await GetAllOrdersFromCache();
        var existingOrder = orders.FirstOrDefault(o => o.Id == order.Id);
        if (existingOrder != null)
        {
            orders.Remove(existingOrder);
        }
        orders.Add(order);
        //TODO WHAT HAPPENS WHEN EXPIRES BEFORE THE REFRESH DUE TO A POSSIBLE(?) SCHEDULING AND THEN THE SCREEN TRIES TO GET THE DATA IN BETWEEN?
        cache.Set(_cacheKey, orders, TimeSpan.FromMinutes(15));
    }
    
    public async Task DeleteOrderAsync(int id)
    {
        var orders = await GetAllOrdersFromCache();
        var order = orders.FirstOrDefault(o => o.Id == id);
        if (order != null)
        {
            orders.Remove(order);
            cache.Set(_cacheKey, orders, TimeSpan.FromMinutes(15));
        }
    }
    
    private async Task<List<OrderDto>> GetAllOrdersFromCache(int companyId = 1)
    {
        if (cache.TryGetValue(_cacheKey, out List<OrderDto> orders)) return orders;
        orders = new List<OrderDto>();
        cache.Set(_cacheKey, orders, TimeSpan.FromMinutes(15));

        return orders;
    }
}
