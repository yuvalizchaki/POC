using MediatR;
using POC.Contracts.CrmDTOs;

namespace POC.App.Commands.OrderAdded;

public class OrderAddedCommand(CrmOrder crmOrder) : IRequest
{
    public CrmOrder CrmOrder { get; set; } = crmOrder;
}