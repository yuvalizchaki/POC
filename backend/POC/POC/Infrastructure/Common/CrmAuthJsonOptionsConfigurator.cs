using System.Text.Json;

namespace POC.Infrastructure.Common;

public class SnakeCaseNamingPolicy : JsonNamingPolicy
{
    public override string ConvertName(string name)
    {
        if (string.IsNullOrEmpty(name))
            return name;

        return string.Concat(name.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x : x.ToString())).ToLower();
    }
}

/** Used for the /auth endpoint (snake_case) */
public class CrmAuthJsonOptionsConfigurator
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
        options.PropertyNamingPolicy = new SnakeCaseNamingPolicy();
        options.PropertyNameCaseInsensitive = true;


        // Add other configurations as needed
    }
}