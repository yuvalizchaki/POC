using Microsoft.Extensions.Caching.Memory;
using POC.Api.Hubs;
using POC.Contracts.CrmDTOs;
using POC.Infrastructure.Common.Notifiers;
using POC.Infrastructure.IRepositories;


namespace POC.Infrastructure.Repositories;

public class InMemoryOrderRepository(
    IMemoryCache cache,
    ILogger<InMemoryOrderRepository> logger
    ) : IOrderRepository
{

    private readonly string _cacheKey = "Orders";
    private int expirationMinutes = 15; //TODO PUT IT IN A PLACE MORE FITTING
    private NotifyOnOrdersChanged? notifyOnOrdersChanged;
    
    public void SetNotifyOnOrdersChanged(NotifyOnOrdersChanged notifyOnOrdersChanged)
    {
        this.notifyOnOrdersChanged = notifyOnOrdersChanged;
    }
    
    public async Task<IEnumerable<CrmOrder>> GetAllOrdersAsync(int companyId)
    {
        return await GetAllOrdersFromCache(companyId);
    }

    public Task SetAllOrdersAsync(IEnumerable<CrmOrder> orders)
    {
        var cacheEntryOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(expirationMinutes)
        };
        cache.Set(_cacheKey, orders, cacheEntryOptions);
        
        notifyOnOrdersChanged?.NotifyAsync();

        return Task.CompletedTask;
    }
    
    public async Task AddOrUpdateOrderAsync(CrmOrder order)
    {
        var orders = await GetAllOrdersFromCache();
        var existingOrder = orders.FirstOrDefault(o => o.Id == order.Id);
        if (existingOrder != null)
        {
            orders.Remove(existingOrder);
        }
        orders.Add(order);
        //TODO WHAT HAPPENS WHEN EXPIRES BEFORE THE REFRESH DUE TO A POSSIBLE(?) SCHEDULING AND THEN THE SCREEN TRIES TO GET THE DATA IN BETWEEN?
        cache.Set(_cacheKey, orders, TimeSpan.FromMinutes(expirationMinutes));
    }

    public Task<CrmOrder?> GetOrderAsync(int id)
    {
        var orders = GetAllOrdersFromCache().Result;
        return Task.FromResult(orders.FirstOrDefault(o => o.Id == id));
    }

    public async Task DeleteOrderAsync(int id)
    {
        var orders = await GetAllOrdersFromCache();
        var order = orders.FirstOrDefault(o => o.Id == id);
        if (order != null)
        {
            orders.Remove(order);
            cache.Set(_cacheKey, orders, TimeSpan.FromMinutes(expirationMinutes));
        }
    }
    
    private async Task<List<CrmOrder>> GetAllOrdersFromCache(int companyId = 1)
    {
        if (cache.TryGetValue(_cacheKey, out List<CrmOrder> orders)) return orders;
        orders = new List<CrmOrder>();
        cache.Set(_cacheKey, orders, TimeSpan.FromMinutes(expirationMinutes));

        return orders;
    }

    
}

