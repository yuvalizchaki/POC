using POC.Contracts.CrmDTOs;
using POC.Infrastructure.Common.Notifiers;

namespace POC.Infrastructure.IRepositories;

public interface IOrderRepository
{
    Task<IEnumerable<CrmOrder>> GetAllOrdersAsync(int companyId);
    
    Task SetAllOrdersAsync(IEnumerable<CrmOrder> orders);
    Task AddOrUpdateOrderAsync(CrmOrder order);
    Task<CrmOrder?> GetOrderAsync(int id);
    // Other relevant methods
    Task DeleteOrderAsync(int id);
    
    void SetNotifyOnOrdersChanged(NotifyOnOrdersChanged notifyOnOrdersChanged);
}