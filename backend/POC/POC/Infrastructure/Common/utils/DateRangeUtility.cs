using POC.Contracts.CrmDTOs;
using POC.Infrastructure.Common.Constants;
using POC.Infrastructure.Models;

namespace POC.Infrastructure.Common.utils;

public static class DateRangeUtility
{
    private const string Format = "yyyy-MM-ddTHH:mm";

    public static bool IsBetween(OrderDto orderDto, TimeEncapsulated timeEncapsulated, DateTime referenceDateTime)
    {
        // If the time encapsulated is not valid, return false
        if (timeEncapsulated is not { From: not null, To: not null }) return false;
        
        var fromDateString = timeEncapsulated.From.ToFormattedDateTime(referenceDateTime, Format);
        var toDateString = timeEncapsulated.To.ToFormattedDateTime(referenceDateTime, Format);

        var startDate = DateTime.ParseExact(fromDateString, Format, null);
        var endDate = DateTime.ParseExact(toDateString, Format, null);
        
        var orderStartDate = orderDto.CrmOrder.StartDate;
        var orderEndDate = orderDto.CrmOrder.EndDate;

        var isOrderStartDateInRange = IsWithinRange(orderStartDate, startDate, endDate);
        var isOrderEndDateInRange = IsWithinRange(orderEndDate, startDate, endDate);

        return timeEncapsulated.Include switch
        {
            TimeInclude.Both => (isOrderStartDateInRange && IsIncomingOrder(orderDto)) ||
                                (isOrderEndDateInRange && IsOutgoingOrder(orderDto)),
            TimeInclude.Incoming => isOrderStartDateInRange && IsIncomingOrder(orderDto),
            TimeInclude.Outgoing => isOrderEndDateInRange && IsOutgoingOrder(orderDto),
            _ => false
        };

        // Local functions, apparently a thing in C# 7.0
        bool IsOutgoingOrder(OrderDto order) => order.TransportType == OrderTransportType.Outgoing;
        bool IsWithinRange(DateTime date, DateTime start, DateTime end) => date >= start && date <= end;
        bool IsIncomingOrder(OrderDto order) => order.TransportType == OrderTransportType.Incoming;
    }
}