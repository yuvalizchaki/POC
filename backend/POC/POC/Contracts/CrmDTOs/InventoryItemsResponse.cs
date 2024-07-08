using POC.Infrastructure.Models;

namespace POC.Contracts.CrmDTOs;

public class InventoryItemsResponse
{
    public InventoryItemDto InventoryItem { get; set; }
    public int Quantity { get; set; }
}