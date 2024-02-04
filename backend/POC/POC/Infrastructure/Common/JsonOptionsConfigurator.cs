using System.Text.Json;

namespace POC.Infrastructure.Common;

public static class JsonOptionsConfigurator
{
    public static JsonSerializerOptions GetConfiguredOptions()
    {
        var options = new JsonSerializerOptions();
        ConfigureJsonOptions(options);
        return options;
    }

    public static void ConfigureJsonOptions(JsonSerializerOptions options)
    {
        // Use camel case naming policy
        options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        // Add other configurations as needed
    }
}
