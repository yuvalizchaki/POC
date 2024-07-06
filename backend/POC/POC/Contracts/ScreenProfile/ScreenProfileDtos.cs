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
    
    public int CompanyId { get; set; } = 1;

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
    [ValidateTimeEncapsulatedDto(ErrorMessage = "Invalid TimeEncapsulated.")]
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
    public TimeRangePart? From { get; set; } = null!;
    public TimeRangePart? To { get; set; } = null!;
}

public class InventoryFilteringDto
{
    public List<int>? EntityIds { get; set; }
}

public class DisplayConfigDto
{
    [Required(ErrorMessage = "IsPaging is required.")]
    public bool IsPaging { get; set; }
    [Required(ErrorMessage = "DisplayTemplate is required.")]
    public DisplayTemplateType DisplayTemplate { get; set; } // Enum: Table, Graph, Notes, whatever
}