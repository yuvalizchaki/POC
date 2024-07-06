namespace POC.Infrastructure.Common.Constants;

public enum TimeUnit
{
    Hour = 1,
    Day = 2,
    Week = 3,
    Month = 4,
    Year = 5
}

public enum Mode
{
    Start = 1,
    Fixed = 2,
    End = 3
}

public enum TimeInclude { 
    Incoming = 1,
    Outgoing = 2,
    Both = 3
}