using POC.Infrastructure.Common.Constants;
using POC.Infrastructure.Models;

namespace POC.Contracts.CrmDTOs;

public class InventoryItemDto
{
    public CrmInventoryItem CrmInventoryItem { get; set; }
    public OrderTransportType TransportType { get; set; }
}