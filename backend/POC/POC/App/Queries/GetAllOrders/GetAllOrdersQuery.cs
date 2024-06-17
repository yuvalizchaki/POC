using MediatR;
using POC.Contracts.CrmDTOs;

namespace POC.App.Queries.GetAllOrders;

public class GetAllOrdersQuery: IRequest<List<OrderDto>>
{
    public int profileId { get; set; }
    public int companyId { get; set; }

    public GetAllOrdersQuery(int profileId, int companyId)
    {
        this.profileId = profileId;
        this.companyId = companyId;
    }
}