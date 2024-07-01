using POC.Contracts.CrmDTOs;

namespace POC.Infrastructure.IRepositories;

public interface IOrderRepository
{
    Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
    
    Task SetAllOrdersAsync(IEnumerable<OrderDto> orders);
    Task AddOrUpdateOrderAsync(OrderDto order);
    // Other relevant methods
    Task DeleteOrderAsync(int id);
}