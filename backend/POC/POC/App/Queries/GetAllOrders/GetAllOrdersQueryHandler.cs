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


    // TODO: Implement CRM adapter and related classes
    public async Task<List<OrderDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        int companyId = 1;
        string queryContent = """
        {
            "skipCount": true,
            "framing": {
                "skip": 0,
                "take": 10
            },
            "sorts": [
                {
                    "sortBy": "Id",
                    "inverseOrder": true
                }
            ],
            "filters": []
        }
        """;
        return await _crmAdapter.GetAllOrdersAsync(companyId, queryContent);

    }


}