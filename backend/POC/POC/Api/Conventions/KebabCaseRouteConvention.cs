namespace POC.Api.Conventions;

using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Text.RegularExpressions;

public class KebabCaseRouteConvention : IControllerModelConvention
{
    public void Apply(ControllerModel controller)
    {
        // Convert the controller name from PascalCase to kebab-case
        controller.Selectors[0].AttributeRouteModel = new AttributeRouteModel
        {
            Template = Regex.Replace(controller.ControllerName, "(?<!^)([A-Z])", "-$1").ToLower()
        };
    }
}