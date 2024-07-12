using POC.Contracts.CrmDTOs;

namespace POC.Infrastructure.Adapters;

public interface IOrderAdapter
{
    public Task<List<CrmOrder>> FetchOrdersAsync(int companyId = 1);
}