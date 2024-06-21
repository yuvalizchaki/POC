namespace POC.Infrastructure.Common.Exceptions;

public class AdminLoginException: Exception
{
    public AdminLoginException()
        : base("Admin login failed")
    {
    }

    public AdminLoginException(string message)
        : base(message)
    {
    }

    public AdminLoginException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
    
}