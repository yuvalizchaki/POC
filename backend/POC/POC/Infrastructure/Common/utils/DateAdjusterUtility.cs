using POC.Infrastructure.Common.Constants;

namespace POC.Infrastructure.Common.utils;

public static class DateAdjusterUtility
{
    public static DateTime AdjustToStart(DateTime referenceDate, TimeUnit unit)
    {
        return unit switch
        {
            TimeUnit.Hour => new DateTime(referenceDate.Year, referenceDate.Month, referenceDate.Day, referenceDate.Hour, 0, 0),
            TimeUnit.Day => new DateTime(referenceDate.Year, referenceDate.Month, referenceDate.Day, 0, 0, 0),
            TimeUnit.Week => referenceDate.AddDays(-(int)referenceDate.DayOfWeek).Date,
            TimeUnit.Month => new DateTime(referenceDate.Year, referenceDate.Month, 1),
            TimeUnit.Year => new DateTime(referenceDate.Year, 1, 1),
            _ => throw new ArgumentException("Invalid time unit specified")
        };
    }

    public static DateTime AdjustToEnd(DateTime referenceDate, TimeUnit unit)
    {
        return unit switch
        {
            TimeUnit.Hour => new DateTime(referenceDate.Year, referenceDate.Month, referenceDate.Day, referenceDate.Hour, 0, 0).AddHours(1).AddTicks(-1),
            TimeUnit.Day => new DateTime(referenceDate.Year, referenceDate.Month, referenceDate.Day, 0, 0, 0).AddDays(1).AddTicks(-1),
            TimeUnit.Week => referenceDate.AddDays(-(int)referenceDate.DayOfWeek + 7).Date.AddTicks(-1),
            TimeUnit.Month => new DateTime(referenceDate.Year, referenceDate.Month, 1).AddMonths(1).AddTicks(-1),
            TimeUnit.Year => new DateTime(referenceDate.Year, 1, 1).AddYears(1).AddTicks(-1),
            _ => throw new ArgumentException("Invalid time unit specified")
        };
    }
}
