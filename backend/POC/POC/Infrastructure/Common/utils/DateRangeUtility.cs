using POC.Infrastructure.Common.Constants;
using POC.Infrastructure.Models;

namespace POC.Infrastructure.Common.utils;

public static class DateRangeUtility
{
    private const string Format = "yyyy-MM-ddTHH:mm";

    public static bool IsBetween(DateTime orderStartDate, DateTime orderEndDate, TimeEncapsulated timeEncapsulated, DateTime referenceDateTime)
    {
        // If the time encapsulated is not valid, return false
        if (timeEncapsulated is not { From: not null, To: not null }) return false;
        
        var fromDateString = timeEncapsulated.From.ToFormattedDateTime(referenceDateTime, Format);
        var toDateString = timeEncapsulated.To.ToFormattedDateTime(referenceDateTime, Format);

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