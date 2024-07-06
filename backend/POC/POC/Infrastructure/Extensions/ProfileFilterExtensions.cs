using Microsoft.IdentityModel.Tokens;
using POC.Contracts.CrmDTOs;
using POC.Contracts.ScreenProfile;
using POC.Infrastructure.Common.utils;
using POC.Infrastructure.Models;
using POC.Infrastructure.Models.CrmSearchQuery;

namespace POC.Infrastructure.Extensions;
public static class ScreenProfileFilterExtensions
{
    public static ScreenProfileFilteringDto ToScreenProfileFilteringDto(this ScreenProfileFiltering screenProfileFilter)
    {
        return new ScreenProfileFilteringDto
        {
            OrderFiltering = screenProfileFilter.OrderFiltering.ToOrderFilteringDto(),
            InventoryFiltering = screenProfileFilter.InventoryFiltering?.ToInventoryFilteringDto(),
            InventorySorting = screenProfileFilter.InventorySorting,
            DisplayConfig = screenProfileFilter.DisplayConfig.ToDisplayConfigDto()
        };
    }

    private static OrderFilteringDto ToOrderFilteringDto(this OrderFiltering orderFiltering)
    {
        return new OrderFilteringDto
        {
            TimeRanges = orderFiltering.TimeRanges.ToTimeEncapsulatedDto(),
            OrderStatuses = orderFiltering.OrderStatuses,
            IsPickup = orderFiltering.IsPickup,
            IsSale = orderFiltering.IsSale,
            EntityIds = orderFiltering.EntityIds,
            Tags = orderFiltering.Tags
        };
    }
    
    private static TimeEncapsulatedDto ToTimeEncapsulatedDto(this TimeEncapsulated timeEncapsulated)
    {
        return new TimeEncapsulatedDto
        {
            From = timeEncapsulated.From,
            To = timeEncapsulated.To
        };
    }

    private static InventoryFilteringDto ToInventoryFilteringDto(this InventoryFiltering inventoryFiltering)
    {
        return new InventoryFilteringDto
        {
            EntityIds = inventoryFiltering.EntityIds
        };
    }

    private static DisplayConfigDto ToDisplayConfigDto(this DisplayConfig displayConfig)
    {
        return new DisplayConfigDto
        {
            IsPaging = displayConfig.IsPaging,
            DisplayTemplate = displayConfig.DisplayTemplate
        };
    }
}

public static class ScreenProfileFilterDtoExtensions
{
    public static ScreenProfileFiltering ToScreenProfileFiltering(this ScreenProfileFilteringDto screenProfileFilterDto)
    {
        return new ScreenProfileFiltering
        {
            OrderFiltering = screenProfileFilterDto.OrderFiltering.ToOrderFiltering(),
            InventoryFiltering = screenProfileFilterDto.InventoryFiltering?.ToInventoryFiltering(),
            InventorySorting = screenProfileFilterDto.InventorySorting,
            DisplayConfig = screenProfileFilterDto.DisplayConfig.ToDisplayConfig()
        };
    }

    private static OrderFiltering ToOrderFiltering(this OrderFilteringDto orderFilteringDto)
    {
        return new OrderFiltering
        {
            TimeRanges = orderFilteringDto.TimeRanges.ToTimeEncapsulated(),
            OrderStatuses = orderFilteringDto.OrderStatuses,
            IsPickup = orderFilteringDto.IsPickup,
            IsSale = orderFilteringDto.IsSale,
            EntityIds = orderFilteringDto.EntityIds,
            Tags = orderFilteringDto.Tags
        };
    }
    
    private static TimeEncapsulated ToTimeEncapsulated(this TimeEncapsulatedDto timeEncapsulatedDto)
    {
        return new TimeEncapsulated
        {
            From = timeEncapsulatedDto.From,
            To = timeEncapsulatedDto.To
        };
    }

    private static InventoryFiltering ToInventoryFiltering(this InventoryFilteringDto inventoryFilteringDto)
    {
        return new InventoryFiltering
        {
            EntityIds = inventoryFilteringDto.EntityIds
        };
    }

    private static DisplayConfig ToDisplayConfig(this DisplayConfigDto displayConfigDto)
    {
        return new DisplayConfig
        {
            IsPaging = displayConfigDto.IsPaging,
            DisplayTemplate = displayConfigDto.DisplayTemplate
        };
    }
}


public static class ScreenProfileFilteringExtensions
{
    private const string Format = "yyyy-MM-ddTHH:mm";

    //TODO notice that it already assumes here the company id of orders.
    public static bool IsMatch(this ScreenProfileFiltering screenProfileFiltering, OrderDto order)
    {
        var orderFiltering = screenProfileFiltering.OrderFiltering;
        return ((orderFiltering.TimeRanges.From == null||IsBetween(order.StartDate, orderFiltering.TimeRanges.From)) || 
                (orderFiltering.TimeRanges.To == null||IsBetween(order.EndDate, orderFiltering.TimeRanges.To))) &&
               (orderFiltering.OrderStatuses == null || orderFiltering.OrderStatuses.Contains(order.Status)) &&
               (orderFiltering.IsPickup == null || orderFiltering.IsPickup == order.IsPickup) &&
               (orderFiltering.EntityIds == null || orderFiltering.EntityIds.Contains(order.DepartmentId));
        //TODO check how to filter by tags
    }
    
    public static bool IsInventoryMatch(this ScreenProfileFiltering screenProfileFiltering, InventoryItemDto orderItem)
    {
        var inventoryFiltering = screenProfileFiltering.InventoryFiltering;
        return inventoryFiltering == null || 
               inventoryFiltering.EntityIds.IsNullOrEmpty() ||
               inventoryFiltering.EntityIds!.Contains(orderItem.DepartmentId);
    }
    
    private static bool IsBetween(DateTime date, TimeRangePart timeRangePart)
    {
        var (start, end) = timeRangePart.ToFormattedDateTime(DateTime.Now, Format);
        var startDate = DateTime.ParseExact(start, Format, null);
        var endDate = DateTime.ParseExact(end, Format, null);
        return date >= startDate && date <= endDate;
    }
}