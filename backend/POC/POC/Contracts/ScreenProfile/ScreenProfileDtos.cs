using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic;
using POC.Contracts.Screen;
using POC.Infrastructure.Common.Attributes;
using POC.Infrastructure.Common.Constants;
using POC.Infrastructure.Common.utils;

namespace POC.Contracts.ScreenProfile;

public class ScreenProfileDto
{
    // TODO: Implement CRM adapter and related classes
    
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    public int CompanyId { get; set; } 

    public List<ScreenDto> Screens { get; set; } = [];
    
    public ScreenProfileFilteringDto ScreenProfileFiltering { get; set; } = null!;

}



public class ScreenProfileFilteringDto
{
    [Required(ErrorMessage = "ScreenProfileFiltering is required.")]
    public OrderFilteringDto OrderFiltering { get; set; } = new OrderFilteringDto();
    public InventoryFilteringDto? InventoryFiltering { get; set; }
    public List<string>? InventorySorting { get; set; } = new List<string>();
    [Required(ErrorMessage = "DisplayConfig is required.")]
    public DisplayConfigDto DisplayConfig { get; set; } = new DisplayConfigDto();
}


public class OrderFilteringDto
{
    public TimeEncapsulatedDto TimeRanges { get; set; } = new TimeEncapsulatedDto();
    
    [ValidateUnitEnumList(ErrorMessage = "Invalid OrderStatus list.")]
    public List<OrderStatus>? OrderStatuses { get; set; }
    public bool? IsPickup { get; set; }
    public bool? IsSale { get; set; } 
    public List<int>? EntityIds { get; set; }
    
    [ValidateUnitEnumList(ErrorMessage = "Invalid OrderType list.")]
    public List<OrderTags>? Tags { get; set; } 
}

public class TimeEncapsulatedDto
{
    [Required(ErrorMessage = "From is required.")]
    public TimeRangePart From { get; set; } = null!;
    [Required(ErrorMessage = "To is required.")]
    public TimeRangePart To { get; set; } = null!;
    
    [ValidateEnum(ErrorMessage = "Invalid include specified")]
    public TimeInclude Include { get; set; }
}

public class InventoryFilteringDto
{
    public List<int>? EntityIds { get; set; }
}

public class DisplayConfigDto
{
    [Required(ErrorMessage = "IsPaging is required.")]
    public bool IsPaging { get; set; }
    
    [ValidateEnum(ErrorMessage = "Invalid DisplayType specified")]
    public DisplayTemplateType DisplayTemplate { get; set; } // Enum: Orders, Inventory
    
    public string? Label { get; set; } 
    
    public int? PagingRefreshTime { get; set; }
    
    public bool IsDarkMode { get; set; }
    
}