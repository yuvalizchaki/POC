namespace POC.Infrastructure.Common.Exceptions;

public class CrmAdapterError : Exception
{
    public CrmAdapterError()
        : base("Crm adapter error.")
    {
    }

    public CrmAdapterError(string message)
        : base(message)
    {
    }

    public CrmAdapterError(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}