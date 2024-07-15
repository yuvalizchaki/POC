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
        var orders = await GetAllOrdersFromCache();
        return orders.TryGetValue(companyId, out IEnumerable<CrmOrder>? value) ? value : new List<CrmOrder>();
    }

    public Task SetAllOrdersAsync(Dictionary<int,IEnumerable<CrmOrder>> orders)
    {
        var cacheEntryOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(expirationMinutes)
        };
        cache.Set(_cacheKey, orders, cacheEntryOptions);
        
        notifyOnOrdersChanged?.NotifyAsync();

        return Task.CompletedTask;
    }
    
    public async Task AddOrUpdateOrderAsync(int companyId, CrmOrder order)
    {
        var ordersDict = await GetAllOrdersFromCache();
        var orders = ordersDict.TryGetValue(companyId, out IEnumerable<CrmOrder>? value) ? value.ToList() : new List<CrmOrder>();
        var existingOrder = orders.FirstOrDefault(o => o.Id == order.Id);
        if (existingOrder != null)
        {
            orders.Remove(existingOrder);
        }
        orders.Add(order);
        ordersDict[companyId] = orders;
        //TODO WHAT HAPPENS WHEN EXPIRES BEFORE THE REFRESH DUE TO A POSSIBLE(?) SCHEDULING AND THEN THE SCREEN TRIES TO GET THE DATA IN BETWEEN?
        cache.Set(_cacheKey, ordersDict, TimeSpan.FromMinutes(expirationMinutes));
    }

    public Task<CrmOrder?> GetOrderAsync(int companyId, int id)
    {
        var ordersDict = GetAllOrdersFromCache().Result;
        return !ordersDict.TryGetValue(companyId, out IEnumerable<CrmOrder>? orders) ? Task.FromResult<CrmOrder?>(null) : Task.FromResult(orders.FirstOrDefault(o => o.Id == id));
    }

    public async Task DeleteOrderAsync(int companyId, int id)
    {
        var ordersDict = await GetAllOrdersFromCache();
        if (!ordersDict.TryGetValue(companyId, out IEnumerable<CrmOrder>? orders)) return;
        var order = orders.FirstOrDefault(o => o.Id == id);
        if (order != null)
        {
            orders = orders.Where(o => o.Id != id);
            ordersDict[companyId] = orders;
            cache.Set(_cacheKey, ordersDict, TimeSpan.FromMinutes(expirationMinutes));
        }
    }
    
    private async Task<Dictionary<int,IEnumerable<CrmOrder>>> GetAllOrdersFromCache()
    {
        if (cache.TryGetValue(_cacheKey, out Dictionary<int,IEnumerable<CrmOrder>> orders)) return orders;
        orders = new Dictionary<int, IEnumerable<CrmOrder>>();
        cache.Set(_cacheKey, orders, TimeSpan.FromMinutes(expirationMinutes));

        return orders;
    }

    
}

