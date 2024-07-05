using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic;
using POC.Infrastructure.Common.Attributes;
using POC.Infrastructure.Common.Constants;
using POC.Infrastructure.Common.utils;

namespace POC.Infrastructure.Models;

public class ScreenProfile
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int CompanyId { get; set; } = 1;
    public ScreenProfileFiltering ScreenProfileFiltering { get; set; } = new ScreenProfileFiltering();
    public List<Screen> Screens { get; set; } = [];
}


public class ScreenProfileFiltering
{
    public OrderFiltering OrderFiltering { get; set; } = new OrderFiltering();
    public InventoryFiltering? InventoryFiltering { get; set; }
    public List<string>? InventorySorting { get; set; }
    public DisplayConfig DisplayConfig { get; set; } = new DisplayConfig();
}


public class OrderFiltering
{
    public TimeRangePart From { get; set; } = null!;
    public TimeRangePart To { get; set; } = null!;
    
    public List<OrderStatus>? OrderStatuses { get; set; }
    public bool? IsSale { get; set; } 
    public bool? IsPickup { get; set; }

    public List<int>? EntityIds { get; set; }
    
    public List<OrderTags>? Tags { get; set; }
}

public class InventoryFiltering
{
    public List<int>? EntityIds { get; set; }
}

public class DisplayConfig
{
    public bool IsPaging { get; set; }
    public DisplayTemplateType DisplayTemplate { get; set; } // Enum: Table, Graph, Notes, whatever
}