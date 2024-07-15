using MediatR;
using POC.Contracts.CrmDTOs;

namespace POC.App.Commands.OrderDeleted;

public class OrderDeletedCommand(
    int id,
    int companyId
    ) : IRequest
{
    public int Id { get; } = id;
    
    public int CompanyId { get; set; } = companyId;
}
