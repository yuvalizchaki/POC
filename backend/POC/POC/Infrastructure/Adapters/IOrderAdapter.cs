using POC.Contracts.CrmDTOs;

namespace POC.Infrastructure.Adapters;

public interface IOrderAdapter
{
    public Task<List<OrderDto>> FetchOrdersAsync(int companyId = 0);
}