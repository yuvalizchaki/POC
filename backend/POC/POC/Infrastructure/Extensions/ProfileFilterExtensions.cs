using System.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using POC.Contracts.CrmDTOs;
using POC.Contracts.ScreenProfile;
using POC.Infrastructure.Common.Constants;
using POC.Infrastructure.Common.utils;
using POC.Infrastructure.Models;
using POC.Infrastructure.Models.CrmSearchQuery;
using StackExchange.Redis;

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
            To = timeEncapsulated.To,
            Include = timeEncapsulated.Include
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
            DisplayTemplate = displayConfig.DisplayTemplate,
            Label = displayConfig.Label,
            PagingRefreshTime = displayConfig.PagingRefreshTime,
            IsDarkMode = displayConfig.IsDarkMode
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
            To = timeEncapsulatedDto.To,
            Include = timeEncapsulatedDto.Include
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
            DisplayTemplate = displayConfigDto.DisplayTemplate,
            Label = displayConfigDto.Label,
            PagingRefreshTime = displayConfigDto.PagingRefreshTime,
            IsDarkMode = displayConfigDto.IsDarkMode
        };
    }
}


public static class ScreenProfileFilteringExtensions
{
    public static bool IsProfileInterestedInOrders(this ScreenProfileFiltering screenProfileFiltering)
    {
        return screenProfileFiltering.DisplayConfig.DisplayTemplate == DisplayTemplateType.Orders;
    }
    
    public static bool IsProfileInterestedInInventoryItems(this ScreenProfileFiltering screenProfileFiltering)
    {
        return screenProfileFiltering.DisplayConfig.DisplayTemplate == DisplayTemplateType.Inventory;
    }
    
    public static bool IsInventoryMatch(this ScreenProfileFiltering screenProfileFiltering, CrmInventoryItem orderItem)
    {
        var inventoryFiltering = screenProfileFiltering.InventoryFiltering;
        return inventoryFiltering?.EntityIds == null || inventoryFiltering.EntityIds.Count == 0 ||
               inventoryFiltering.EntityIds!.Contains(orderItem.DepartmentId);
    }
    
    public static bool IsOrderMatch(this ScreenProfileFiltering screenProfileFiltering, OrderDto orderDto){
        var order = orderDto.CrmOrder;
        var orderFiltering = screenProfileFiltering.OrderFiltering;
        return DateRangeUtility.IsBetween(orderDto ,screenProfileFiltering.OrderFiltering.TimeRanges, DateTime.Now) &&
               (orderFiltering.OrderStatuses == null || orderFiltering.OrderStatuses.Count == 0 || orderFiltering.OrderStatuses.Contains(order.Status)) &&
               (orderFiltering.IsPickup == null || orderFiltering.IsPickup == order.IsPickup) &&
               (orderFiltering.EntityIds == null || orderFiltering.EntityIds.Count == 0 || orderFiltering.EntityIds.Contains(order.DepartmentId));
    }
    
}