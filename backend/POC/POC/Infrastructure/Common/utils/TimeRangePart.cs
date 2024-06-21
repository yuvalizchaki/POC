namespace POC.Infrastructure.Common.utils;

public class TimeRangePart
{
    public TimeUnit Unit { get; set; } // Example: "day"
    public Mode Mode { get; set; } // Example: 1 - start, 2 - end, 3 - fixed
    public int Amount { get; set; }
}

//extension of converting into time of a given format
public static class TimeRangePartExtensions
{
    public static (string start, string end) ToFormattedDateTime(this TimeRangePart timeRangePart, DateTime referenceDate, string format)
    {
        var startDate = timeRangePart.Mode switch
        {
            Mode.Start => AdjustToStart(referenceDate, timeRangePart.Unit),
            Mode.End => AdjustToEnd(referenceDate, timeRangePart.Unit),
            _ => throw new ArgumentException("Invalid mode specified")
        };
        var endDate = timeRangePart.Unit switch
        {
            TimeUnit.Hour => startDate.AddHours(timeRangePart.Amount),
            TimeUnit.Day => startDate.AddDays(timeRangePart.Amount),
            TimeUnit.Week => startDate.AddDays(timeRangePart.Amount * 7),
            TimeUnit.Month => startDate.AddMonths(timeRangePart.Amount),
            TimeUnit.Year => startDate.AddYears(timeRangePart.Amount),
            _ => throw new ArgumentException("Invalid time unit specified")
        };
        
        return (startDate.ToString(format), endDate.ToString(format));
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
                return new DateTime(referenceDate.Year, referenceDate.Month, referenceDate.Day - delta, 0, 0, 0);
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
                return new DateTime(referenceDate.Year, referenceDate.Month, referenceDate.Day, referenceDate.Hour+1, 0, 0).AddTicks(-1);
            case TimeUnit.Day:
                return new DateTime(referenceDate.Year, referenceDate.Month, referenceDate.Day + 1, 0, 0, 0).AddTicks(-1);
            case TimeUnit.Week:
                var delta = referenceDate.DayOfWeek - DayOfWeek.Sunday;
                return new DateTime(referenceDate.Year, referenceDate.Month, referenceDate.Day - delta + 7, 0, 0, 0).AddTicks(-1);
            case TimeUnit.Month:
                return new DateTime(referenceDate.Year, referenceDate.Month+1, 1).AddTicks(-1);
            case TimeUnit.Year:
                return new DateTime(referenceDate.Year+1, 1, 1).AddTicks(-1);
            default:
                throw new ArgumentException("Invalid time unit specified");
        }
    }
}

public enum TimeUnit
{
    Hour,
    Day,
    Week,
    Month,
    Year
}

public enum Mode
{
    Start = 1,
    End = 2,
    Fixed = 3
}
