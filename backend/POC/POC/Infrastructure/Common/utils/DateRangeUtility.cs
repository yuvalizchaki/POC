using POC.Infrastructure.Common.Constants;
using POC.Infrastructure.Models;

namespace POC.Infrastructure.Common.utils;

public static class DateRangeUtility
{
    private const string Format = "yyyy-MM-ddTHH:mm";

    public static bool IsBetween(DateTime orderStartDate, DateTime orderEndDate, TimeEncapsulated timeEncapsulated)
    {
        // If the time encapsulated is not valid, return false
        if (timeEncapsulated is not { From: not null, To: not null }) return false;
        
        var fromDateString = timeEncapsulated.From.ToFormattedDateTime(DateTime.Now, Format);
        var toDateString = timeEncapsulated.To.ToFormattedDateTime(DateTime.Now, Format);

        var startDate = DateTime.ParseExact(fromDateString, Format, null);
        var endDate = DateTime.ParseExact(toDateString, Format, null);

        return timeEncapsulated.Include switch
        {
            TimeInclude.Both => (orderStartDate >= startDate && orderStartDate <= endDate) ||
                                (orderEndDate >= startDate && orderEndDate <= endDate),
            TimeInclude.Incoming => orderStartDate >= startDate && orderStartDate <= endDate,
            TimeInclude.Outgoing => orderEndDate >= startDate && orderEndDate <= endDate,
            _ => false
        };

    }
}