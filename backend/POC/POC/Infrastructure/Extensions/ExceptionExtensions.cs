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

public static class IpNotInGuestHubExceptionExtensions
{
    public static ErrorResponse ToErrorResponse(this IpNotInGuestHubException ex)
    {
        return new ErrorResponse(new Dictionary<string, string> {
        {
            nameof(PairScreenDto.PairingCode),
            "Pairing code not found in guest hub."
        } });
    }
}

// public static class ScreenAlreadyPairedExceptionExtensions
// {
//     public static ErrorResponse ToErrorResponse(this ScreenAlreadyPairedException ex)
//     {
//         return new ErrorResponse(new Dictionary<string, string> {
//         {
//             nameof(PairScreenDto.IpAddress),
//             "Screen already paired."
//         } });
//     }
// }

public static class ExceptionExtensions
{
    public static ErrorResponse MapToErrorResponse(this Exception ex)
    {
        return ex switch
        {
            ScreenProfileNotFoundException sException => sException.ToErrorResponse(),
            IpNotInGuestHubException iException => iException.ToErrorResponse(),
            // ScreenAlreadyPairedException sApException => sApException.ToErrorResponse(),
            _ => new ErrorResponse(new Dictionary<string, string> { { "error", ex.Message } })
        };
    }
}