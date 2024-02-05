using System.Collections;
using FluentValidation.Results;

namespace POC.Contracts.Response;

public class ErrorResponse
{
    public Dictionary<string, string> Errors { get; set; }

    public ErrorResponse(List<ValidationFailure> errors)
    {
        Errors = errors.ToDictionary(failure => failure.PropertyName, failure => failure.ErrorMessage);

    }
    
    //dictionary constructor
    public ErrorResponse(Dictionary<string, string> errors)
    {
        Errors = errors;
    }
}