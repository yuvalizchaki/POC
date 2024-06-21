using System.Diagnostics;
using Microsoft.VisualBasic;
using POC.Contracts.ScreenProfile;
using POC.Infrastructure.Common.Constants;
using POC.Infrastructure.Common.utils;
using POC.Infrastructure.Models;
using POC.Infrastructure.Models.CrmSearchQuery;

namespace POC.Infrastructure.Extensions;
public static class ScreenProfileFilterDtoExtensions
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
            From = orderFiltering.From,
            To = orderFiltering.To,
            OrderStatuses = orderFiltering.OrderStatuses,
            IsPickup = orderFiltering.IsPickup,
            IsSale = orderFiltering.IsSale,
            EntityIds = orderFiltering.EntityIds,
            Tags = orderFiltering.Tags
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

public static class ScreenProfileFilterExtensions
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
            From = orderFilteringDto.From,
            To = orderFilteringDto.To,
            OrderStatuses = orderFilteringDto.OrderStatuses,
            IsPickup = orderFilteringDto.IsPickup,
            IsSale = orderFilteringDto.IsSale,
            EntityIds = orderFilteringDto.EntityIds,
            Tags = orderFilteringDto.Tags
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
    static string format = "yyyy-MM-ddTHH:mm";
    static string status = "Status";
    static string isPickup = "IsPickup";
    static string isSale = "IsSale";
    static string entityIds = "DepartmentId";
    static string startDate = "StartDate";
    static string endDate = "EndDate";
    private static string tags = "OrderTagIds";

    public static SearchRequest ToSearchRequest(this ScreenProfileFiltering screenProfileFiltering)
    {
        var searchRequest = SearchRequestBuilder.Empty;
        var from = screenProfileFiltering.OrderFiltering.From;
        var to = screenProfileFiltering.OrderFiltering.To;
        
        var (fromStart, fromEnd) = from.ToFormattedDateTime(DateTime.Now, format);
        var (toStart, toEnd) = to.ToFormattedDateTime(DateTime.Now, format);
        
        // searchRequest = searchRequest.AppendFiltering(
        //     startDate,
        //     FilterOperation.Gt,
        //     fromStart);
        // searchRequest = searchRequest.AppendFiltering(
        //     startDate,
        //     FilterOperation.Lt,
        //     fromEnd);
        //
        // searchRequest = searchRequest.AppendFiltering(
        //     endDate,
        //     FilterOperation.Gt,
        //     toStart);
        // searchRequest = searchRequest.AppendFiltering(
        //     endDate,
        //     FilterOperation.Lt,
        //     toEnd);

        if (screenProfileFiltering.OrderFiltering.OrderStatuses != null)
        {
            var orderStatuses = screenProfileFiltering.OrderFiltering.OrderStatuses.Select(s => s.ToString());
            searchRequest = searchRequest.AppendFiltering(status, FilterOperation.In, orderStatuses.ToArray());
        }

        if (screenProfileFiltering.OrderFiltering.IsPickup!=null)
        {
            searchRequest = searchRequest.AppendFiltering(isPickup, screenProfileFiltering.OrderFiltering.IsPickup.Value? "true" : "false");
        }

        if (screenProfileFiltering.OrderFiltering.IsSale !=null)
        {
            searchRequest = searchRequest.AppendFiltering(isSale, screenProfileFiltering.OrderFiltering.IsSale.Value ?  "true" : "false");
        }

        if (screenProfileFiltering.OrderFiltering.EntityIds != null)
        {
            var entityIdsList = screenProfileFiltering.OrderFiltering.EntityIds.Select(s => s.ToString());
            searchRequest = searchRequest.AppendFiltering(entityIds, FilterOperation.In, entityIdsList.ToArray());
        }
        
        //TODO: see why tags makes problems when the request is sent into the crm server
        // if (screenProfileFiltering.OrderFiltering.Tags != null)
        // {
        //     var tagsInts = screenProfileFiltering.OrderFiltering.Tags.Select(s => (int)s);
        //     var tagsList = tagsInts.Select(s => s.ToString());
        //     searchRequest = searchRequest.AppendFiltering(tags, FilterOperation.In, tagsList.ToArray());
        // }
        
        if (screenProfileFiltering.InventoryFiltering is { EntityIds: not null })
        {
            var entityIdsList = screenProfileFiltering.InventoryFiltering.EntityIds.Select(s => s.ToString());
            searchRequest = searchRequest.AppendFiltering(entityIds, FilterOperation.In, entityIdsList.ToArray());
        }

        return searchRequest.Build();
    }
}