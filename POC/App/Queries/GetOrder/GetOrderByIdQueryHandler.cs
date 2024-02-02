using MediatR;
using POC.Contracts.CrmDTOs;
using POC.Infrastructure.Adapters;

namespace POC.App.Queries.GetOrder;

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDto>
{
    private readonly CrmAdapter _crmAdapter;

    public GetOrderByIdQueryHandler(CrmAdapter crmAdapter)
    {
        _crmAdapter = crmAdapter;
    }

    public async Task<OrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        return await _crmAdapter.GetOrderById(request.Id);
    }

    
}