using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using POC.Contracts.ScreenProfile;

namespace POC.Infrastructure.Common.Attributes;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class ValidateCompanyIdAttribute : TypeFilterAttribute
{
    public ValidateCompanyIdAttribute(string parameterName) : base(typeof(ValidateCompanyIdFilter))
    {
        Arguments = new object[] { parameterName };
    }
}

public class ValidateCompanyIdFilter(string parameterName) : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ActionArguments.TryGetValue(parameterName, out var value) ||
            value is not CreateScreenProfileDto dto) return;
        
        var userCompanyId = context.HttpContext.User.FindFirst("CompanyId")?.Value;

        if (userCompanyId == null || !int.TryParse(userCompanyId, out var parsedCompanyId) || dto.CompanyId != parsedCompanyId)
        {
            context.Result = new UnauthorizedObjectResult("Unauthorized to create screen profile for another company.");
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}
