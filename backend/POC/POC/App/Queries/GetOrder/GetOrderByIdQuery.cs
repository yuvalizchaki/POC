using MediatR;
using POC.Contracts.CrmDTOs;

namespace POC.App.Queries.GetOrder;

public class GetOrderByIdQuery : IRequest<OrderDto>
{
    public int Id { get; set; }

    public GetOrderByIdQuery(int id)
    {
        Id = id;
    }
}