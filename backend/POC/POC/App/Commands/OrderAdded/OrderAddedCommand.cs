using MediatR;
using POC.Contracts.CrmDTOs;

namespace POC.App.Commands.OrderAdded;

public class OrderAddedCommand(OrderDto orderDto) : IRequest
{
    public OrderDto OrderDto { get; set; } = orderDto;
}