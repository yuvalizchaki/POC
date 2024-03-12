namespace POC.Infrastructure.Common.Exceptions;
using System;

public class ScreenNotFoundException : Exception
{
    public ScreenNotFoundException()
        : base("Screen not found.")
    {
    }

    public ScreenNotFoundException(string message)
        : base(message)
    {
    }

    public ScreenNotFoundException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}