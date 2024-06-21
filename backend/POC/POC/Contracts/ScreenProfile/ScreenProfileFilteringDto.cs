using POC.Infrastructure.Common.Constants;
using POC.Infrastructure.Common.utils;
using POC.Infrastructure.Models;

namespace POC.Contracts.ScreenProfile;


public class ScreenProfileFilteringDto
{
    public OrderFilteringDto OrderFiltering { get; set; } = new OrderFilteringDto();
    public InventoryFilteringDto? InventoryFiltering { get; set; }
    public List<string> InventorySorting { get; set; } = new List<string>();
    public DisplayConfigDto DisplayConfig { get; set; } = new DisplayConfigDto();
}


public class OrderFilteringDto
{
    public TimeRangePart From { get; set; } = null!;
    public TimeRangePart To { get; set; } = null!;
    
    public List<OrderStatus>? OrderStatuses { get; set; }
    public bool? IsPickup { get; set; }
    public bool? IsSale { get; set; } 
    public List<int>? EntityIds { get; set; }
    
    public List<OrderTags>? Tags { get; set; } 
}

public class InventoryFilteringDto
{
    public List<int>? EntityIds { get; set; }
}

public class DisplayConfigDto
{
    public bool IsPaging { get; set; }
    public DisplayTemplateType DisplayTemplate { get; set; } // Enum: Table, Graph, Notes, whatever
}