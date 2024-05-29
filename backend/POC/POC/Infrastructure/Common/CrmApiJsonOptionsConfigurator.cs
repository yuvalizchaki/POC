using System.Text.Json;

namespace POC.Infrastructure.Common;

public class UpperCaseCamelCaseNamingPolicy : JsonNamingPolicy
{
    public override string ConvertName(string name)
    {
        if (string.IsNullOrEmpty(name) || char.IsUpper(name[0]))
            return name;

        return char.ToUpper(name[0]) + name.Substring(1);
    }
}

/** Used for the /auth endpoint (snake_case) */
public class CrmApiJsonOptionsConfigurator
{
    public static JsonSerializerOptions GetConfiguredOptions()
    {
        var options = new JsonSerializerOptions();
        ConfigureJsonOptions(options);
        return options;
    }

    public static void ConfigureJsonOptions(JsonSerializerOptions options)
    {
        // Use custom UpperCaseCamelCase naming policy
        options.PropertyNamingPolicy = new UpperCaseCamelCaseNamingPolicy();
        options.PropertyNameCaseInsensitive = true;


        // Add other configurations as needed
    }
}