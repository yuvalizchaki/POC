using MediatR;
using POC.Contracts.CrmDTOs;

namespace POC.App.Commands.OrderUpdated;

public class OrderUpdatedCommand(OrderDto orderDto) : IRequest
{
    public OrderDto OrderDto { get; set; } = orderDto;
}