namespace POC.Infrastructure.Common.Exceptions;

public class TokenError : Exception
{
    public TokenError()
        : base("Token error.")
    {
    }

    public TokenError(string message)
        : base(message)
    {
    }

    public TokenError(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}