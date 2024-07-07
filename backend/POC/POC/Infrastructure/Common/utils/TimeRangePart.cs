using System.ComponentModel.DataAnnotations;
using POC.Infrastructure.Common.Attributes;
using POC.Infrastructure.Common.Constants;

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
        date = timeRangePart.Unit switch
        {
            TimeUnit.Hour => date.AddHours(timeRangePart.Amount),
            TimeUnit.Day => date.AddDays(timeRangePart.Amount),
            TimeUnit.Week => date.AddDays(7 * timeRangePart.Amount),
            TimeUnit.Month => date.AddMonths(timeRangePart.Amount),
            TimeUnit.Year => date.AddYears(timeRangePart.Amount),
            _ => throw new ArgumentException("Invalid time unit specified")
        };
        
        return date.ToString(format);
    }
}


