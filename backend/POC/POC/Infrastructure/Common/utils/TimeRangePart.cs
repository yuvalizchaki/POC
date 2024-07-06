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
            Mode.Start => AdjustToStart(referenceDate, timeRangePart.Unit),
            Mode.Fixed => referenceDate,
            Mode.End => AdjustToEnd(referenceDate, timeRangePart.Unit),
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
    
    
    private static DateTime AdjustToStart(DateTime referenceDate, TimeUnit unit)
    {
        switch (unit)
        {
            case TimeUnit.Hour:
                return new DateTime(referenceDate.Year, referenceDate.Month, referenceDate.Day, referenceDate.Hour, 0, 0);
            case TimeUnit.Day:
                return new DateTime(referenceDate.Year, referenceDate.Month, referenceDate.Day, 0, 0, 0);
            case TimeUnit.Week:
                var delta = referenceDate.DayOfWeek - DayOfWeek.Sunday;
                return new DateTime(referenceDate.Year, referenceDate.Month, referenceDate.Day , 0, 0, 0).AddDays(-delta);
            case TimeUnit.Month:
                return new DateTime(referenceDate.Year, referenceDate.Month, 1);
            case TimeUnit.Year:
                return new DateTime(referenceDate.Year, 1, 1);
            default:
                throw new ArgumentException("Invalid time unit specified");
        }
    }

    private static DateTime AdjustToEnd(DateTime referenceDate, TimeUnit unit)
    {
        switch (unit)
        {
            case TimeUnit.Hour:
                return new DateTime(referenceDate.Year, referenceDate.Month, referenceDate.Day, referenceDate.Hour, 0, 0).AddHours(1).AddTicks(-1);
            case TimeUnit.Day:
                return new DateTime(referenceDate.Year, referenceDate.Month, referenceDate.Day, 0, 0, 0).AddDays(1).AddTicks(-1);
            case TimeUnit.Week:
                var delta = referenceDate.DayOfWeek - DayOfWeek.Sunday;
                return new DateTime(referenceDate.Year, referenceDate.Month, referenceDate.Day , 0, 0, 0).AddDays(- delta + 7).AddTicks(-1);
            case TimeUnit.Month:
                return new DateTime(referenceDate.Year, referenceDate.Month, 1).AddMonths(1).AddTicks(-1);
            case TimeUnit.Year:
                return new DateTime(referenceDate.Year, 1, 1).AddYears(+1).AddTicks(-1);
            default:
                throw new ArgumentException("Invalid time unit specified");
        }
    }
}


