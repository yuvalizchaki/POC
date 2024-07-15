using POC.Contracts.CrmDTOs;
using POC.Infrastructure.Common.Notifiers;

namespace POC.Infrastructure.IRepositories;

public interface IOrderRepository
{
    Task<IEnumerable<CrmOrder>> GetAllOrdersAsync(int companyId);
    
    Task SetAllOrdersAsync(Dictionary<int,IEnumerable<CrmOrder>> orders);
    Task AddOrUpdateOrderAsync(int companyId, CrmOrder order);
    Task<CrmOrder?> GetOrderAsync(int companyId, int id);
    // Other relevant methods
    Task DeleteOrderAsync(int companyId, int id);
    
    void SetNotifyOnOrdersChanged(NotifyOnOrdersChanged notifyOnOrdersChanged);
}