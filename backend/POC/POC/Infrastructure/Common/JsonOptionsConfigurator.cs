using System.Text.Json;
using System.Text.Json.Serialization;

namespace POC.Infrastructure.Common;

public static class JsonOptionsConfigurator
{
    public static void ConfigureJsonOptions(JsonSerializerOptions options)
    {
        // Use camel case naming policy
        options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

        // // Add converters as needed, e.g., for enum strings
        // options.Converters.Add(new JsonStringEnumConverter());

        // Add other options as needed
        // options.WriteIndented = true;
    }
}