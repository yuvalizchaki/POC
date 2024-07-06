using POC.Contracts.CrmDTOs;

namespace POC.Infrastructure.IRepositories;

public interface IOrderRepository
{
    Task<IEnumerable<OrderDto>> GetAllOrdersAsync(int companyId);
    
    Task SetAllOrdersAsync(IEnumerable<OrderDto> orders);
    Task AddOrUpdateOrderAsync(OrderDto order);
    Task<OrderDto?> GetOrderAsync(int id);
    // Other relevant methods
    Task DeleteOrderAsync(int id);
}