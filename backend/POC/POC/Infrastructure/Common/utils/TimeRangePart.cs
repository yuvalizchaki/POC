using System.ComponentModel.DataAnnotations;
using POC.Infrastructure.Common.Attributes;
using POC.Infrastructure.Common.Constants;
using StackExchange.Redis;

namespace POC.Infrastructure.Common.utils;

public class TimeRangePart
{
    
    [ValidateEnum(ErrorMessage = "Invalid time unit specified")]
    public TimeUnit Unit { get; set; } // Example: "day"
    
    [ValidateEnum(ErrorMessage = "Invalid mode specified")]
    public Mode Mode { get; set; } // Example: 1 - start, 2 - fixed, 3 - end
    
    public int Amount { get; set; }
}

//extension of converting into time of a given format
public static class TimeRangePartExtensions
{
    public static string ToFormattedDateTime(this TimeRangePart timeRangePart, DateTime referenceDate, string format)
    {
        var date = timeRangePart.Mode switch
        {
            Mode.Start => DateAdjusterUtility.AdjustToStart(referenceDate, timeRangePart.Unit),
            Mode.Fixed => referenceDate,
            Mode.End => DateAdjusterUtility.AdjustToEnd(referenceDate, timeRangePart.Unit),
            _ => throw new ArgumentException("Invalid mode specified")
        };
        
        if (timeRangePart.Mode != Mode.Fixed) return date.ToString(format);
        
        return timeRangePart.Unit switch
        {
            TimeUnit.Hour => date.AddHours(timeRangePart.Amount).ToString(format),
            TimeUnit.Day => date.AddDays(timeRangePart.Amount).ToString(format),
            TimeUnit.Week => date.AddDays(7 * timeRangePart.Amount).ToString(format),
            TimeUnit.Month => date.AddMonths(timeRangePart.Amount).ToString(format),
            TimeUnit.Year => date.AddYears(timeRangePart.Amount).ToString(format),
            _ => throw new ArgumentException("Invalid time unit specified")
        };
    }
}


