using MediatR;
using POC.Contracts.CrmDTOs;

namespace POC.App.Commands.OrderDeleted;

public class OrderDeletedCommand(OrderDto orderDto) : IRequest
{
    public OrderDto OrderDto { get; set; } = orderDto;
}