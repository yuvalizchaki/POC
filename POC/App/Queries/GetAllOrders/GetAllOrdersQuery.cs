using MediatR;
using POC.Contracts.CrmDTOs;

namespace POC.App.Queries.GetAllOrders;

public class GetAllOrdersQuery: IRequest<List<OrderDto>>
{
    
}