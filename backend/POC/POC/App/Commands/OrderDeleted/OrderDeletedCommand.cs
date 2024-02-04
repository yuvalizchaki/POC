using MediatR;
using POC.Contracts.CrmDTOs;

namespace POC.App.Commands.OrderDeleted;

public class OrderDeletedCommand(int id) : IRequest
{
    public int Id { get; } = id;
}
