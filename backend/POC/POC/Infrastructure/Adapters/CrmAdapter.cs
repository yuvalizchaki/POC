using System.Net;
using System.Text.Json;
using POC.Contracts.CrmDTOs;
using POC.Infrastructure.Common;

namespace POC.Infrastructure.Adapters;

public class CrmAdapter
{
    private readonly HttpClient _crmApiClient;
    private ILogger<CrmAdapter> _logger;
    private JsonSerializerOptions _crmJsonSerializerOptions;
    public CrmAdapter(IHttpClientFactory httpClientFactory, ILogger<CrmAdapter> logger)
    {
        _logger = logger;
        _crmApiClient = httpClientFactory.CreateClient("CrmApiClient");  
        _crmApiClient.BaseAddress = new Uri("http://localhost:8008");
        _crmJsonSerializerOptions = CrmJsonOptionsConfigurator.GetConfiguredOptions(); // Using external Crm format
    }

    // private async Task EnsureCrmApiConnectionAsync()
    // {
    //     var response = await _crmApiClient.GetAsync("health-check");
    //     response.EnsureSuccessStatusCode();
    // }

    public async Task<List<OrderDto>> GetAllOrdersAsync()
    { 
        // await EnsureCrmApiConnectionAsync();
        var response = await _crmApiClient.GetAsync("orders");
        if (response.StatusCode == HttpStatusCode.NotFound)
            throw new HttpRequestException($"Resource not found at {_crmApiClient.BaseAddress}orders");
        _logger.LogInformation($"[DEBUG] crm response: {response}");
        response.EnsureSuccessStatusCode();
        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<OrderDto>>(responseContent, _crmJsonSerializerOptions);
    }

    public async Task<OrderDto> GetOrderById(int id)
    {
        // await EnsureCrmApiConnectionAsync();
        var response = await _crmApiClient.GetAsync($"orders/{id}");
        if (response.StatusCode == HttpStatusCode.NotFound)
            throw new HttpRequestException($"Order with ID {id} not found.");

        response.EnsureSuccessStatusCode();
        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<OrderDto>(responseContent, _crmJsonSerializerOptions);
    }
}

