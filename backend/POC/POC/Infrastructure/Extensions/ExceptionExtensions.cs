using POC.Contracts.Response;
using POC.Contracts.Screen;
using POC.Infrastructure.Common.Exceptions;

namespace POC.Infrastructure.Extensions;


public static class ScreenProfileNotFoundExceptionExtensions
{
    public static ErrorResponse ToErrorResponse(this ScreenProfileNotFoundException ex)
    {
        return new ErrorResponse(new Dictionary<string, string> {
        {
            nameof(PairScreenDto.ScreenProfileId),
            "Screen profile not found."
        } });
    }
}

public static class IncorrectPairingCodeExceptionExtensions
{
    public static ErrorResponse ToErrorResponse(this IncorrectPairingCodeException ex)
    {
        return new ErrorResponse(new Dictionary<string, string> {
        {
            nameof(PairScreenDto.PairingCode),
            "Pairing code is incorrect"
        } });
    }
}

public static class PairingCodeDoesNotExistExceptionExtensions
{
    public static ErrorResponse ToErrorResponse(this PairingCodeDoesNotExistException ex)
    {
        return new ErrorResponse(new Dictionary<string, string> {
        {
            nameof(PairScreenDto.PairingCode),
            "Pairing code has no existing connection."
        } });
    }
}

public static class ExceptionExtensions
{
    public static ErrorResponse MapToErrorResponse(this Exception ex)
    {
        return ex switch
        {
            ScreenProfileNotFoundException sException => sException.ToErrorResponse(),
            IncorrectPairingCodeException iException => iException.ToErrorResponse(),
            PairingCodeDoesNotExistException pcException => pcException.ToErrorResponse(),
            _ => new ErrorResponse(new Dictionary<string, string> { { "error", ex.Message } })
        };
    }
}