using System.Diagnostics;
using Microsoft.VisualBasic;
using POC.Contracts.ScreenProfile;
using POC.Infrastructure.Common.Constants;
using POC.Infrastructure.Models;
using POC.Infrastructure.Models.CrmSearchQuery;

namespace POC.Infrastructure.Extensions;

public static class ScreenProfileFilterDtoExtensions
{
    //to dto
    public static ScreenProfileFilteringDto ToScreenProfileFilteringDto(this ScreenProfileFiltering screenProfileFilter)
    {
        return new ScreenProfileFilteringDto
        {
            OrderTimeRange = screenProfileFilter.OrderTimeRange.ToOrderTimeRangeDto(),
            OrderStatusses = screenProfileFilter.OrderStatusses,
            IsPickup = screenProfileFilter.IsPickup,
            IsSale = screenProfileFilter.IsSale,
            EntityIds = screenProfileFilter.EntityIds
        };

    }
    
}

public static class ScreenProfileFilterExtensions
{
    //to dto
    public static ScreenProfileFiltering ToScreenProfileFiltering(this ScreenProfileFilteringDto screenProfileFilter)
    {
        return new ScreenProfileFiltering
        {
            OrderTimeRange = screenProfileFilter.OrderTimeRange.ToOrderTimeRange(),
            OrderStatusses = screenProfileFilter.OrderStatusses,
            IsPickup = screenProfileFilter.IsPickup,
            IsSale = screenProfileFilter.IsSale,
            EntityIds = screenProfileFilter.EntityIds
        };

    }
    
}

public static class OrderTimeRangeExtensions
{
    //to dto
    public static OrderTimeRangeDto ToOrderTimeRangeDto(this OrderTimeRange orderTimeRange)
    {
        return new OrderTimeRangeDto
        {
            StartDate = orderTimeRange.StartDate,
            EndDate = orderTimeRange.EndDate
        };
    }
}

public static class OrderTimeRangeDtoExtensions
{
    //to dto
    public static OrderTimeRange ToOrderTimeRange(this OrderTimeRangeDto orderTimeRange)
    {
        return new OrderTimeRange
        {
            StartDate = orderTimeRange.StartDate,
            EndDate = orderTimeRange.EndDate
        };
    }
}


public static class ScreenProfileFilteringExtensions
{
    static string format = "yyyy-MM-ddTHH:mm";
    static string status = "Status";
    static string isPickup = "IsPickup";
    static string isSale = "IsSale";
    static string entityIds = "EntityIds";
    static string startDate = "StartDate";
    static string endDate = "EndDate";
    //to dto
    public static SearchRequest ToSearchRequest(this ScreenProfileFiltering screenProfileFilteringDto)
    {
        var searchRequest = SearchRequestBuilder.Empty;
        if (screenProfileFilteringDto.OrderTimeRange != null)
        {
            
            searchRequest = searchRequest.AppendFiltering(
                startDate,
                FilterOperation.Gt,
                screenProfileFilteringDto.OrderTimeRange.StartDate.ToString(format));
            
            searchRequest = searchRequest.AppendFiltering(
                endDate,
                FilterOperation.Lt,
                screenProfileFilteringDto.OrderTimeRange.EndDate.ToString(format));
            
        }
        if (screenProfileFilteringDto.OrderStatusses != null)
        {
            var orderStatusses = screenProfileFilteringDto.OrderStatusses.Select(s =>
            {
                var number = (int) s;
                return number.ToString();
            });
            searchRequest = searchRequest.AppendFiltering(status, FilterOperation.In, orderStatusses.ToArray());
        }
        if (screenProfileFilteringDto.IsPickup !=TriState.UseDefault)
        {
            searchRequest = searchRequest.AppendFiltering(isPickup, screenProfileFilteringDto.IsPickup == TriState.True ? "true" : "false");
        }
        if (screenProfileFilteringDto.IsSale !=TriState.UseDefault)
        {
            searchRequest = searchRequest.AppendFiltering(isSale, screenProfileFilteringDto.IsSale == TriState.True ? "true" : "false");
        }
        // if (screenProfileFilteringDto.EntityIds != null)
        // {
        //     foreach (var entityId in screenProfileFilteringDto.EntityIds)
        //     {
        //         searchRequest = searchRequest.AppendFiltering("EntityIds", entityId.ToString());
        //     }
        // }
        return searchRequest.Build();
    }
}