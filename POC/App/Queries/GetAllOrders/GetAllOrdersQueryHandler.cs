using MediatR;
using POC.App.Queries.GetAllScreenProfiles;
using POC.Contracts.CrmDTOs;
using POC.Infrastructure.Adapters;

namespace POC.App.Queries.GetAllOrders;

public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, List<OrderDto>>
{
    private readonly CrmAdapter _crmAdapter;

    public GetAllOrdersQueryHandler(CrmAdapter crmAdapter)
    {
        _crmAdapter = crmAdapter;
    }

    public async Task<List<OrderDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        return await _crmAdapter.GetAllOrdersAsync();
    }
    
}