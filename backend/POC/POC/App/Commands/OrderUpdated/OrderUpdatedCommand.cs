using MediatR;
using POC.Contracts.CrmDTOs;

namespace POC.App.Commands.OrderUpdated;

public class OrderUpdatedCommand(CrmOrder crmOrder) : IRequest
{
    public CrmOrder CrmOrder { get; set; } = crmOrder;
}