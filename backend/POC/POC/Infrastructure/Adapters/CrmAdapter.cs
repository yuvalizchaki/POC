using System.Net;
using Microsoft.AspNetCore.Http.HttpResults;
using POC.Contracts.CrmDTOs;
using Newtonsoft.Json;

namespace POC.Infrastructure.Adapters;

public class CrmAdapter
{
    private readonly HttpClient _crmApiClient;
    public CrmAdapter(IHttpClientFactory httpClientFactory)
    {
        _crmApiClient = httpClientFactory.CreateClient("CrmApiClient");  
        _crmApiClient.BaseAddress = new Uri("http://localhost:8008/"); // Set the base address
    }
    private async Task<bool> IsCrmApiConnectedAsync()
    {
        try
        {
            // Make a simple health check request
            HttpResponseMessage response = await _crmApiClient.GetAsync("health-check");

            // Check if the request was successful
            response.EnsureSuccessStatusCode();

            // If successful, return true
            return true;
        }
        catch (HttpRequestException)
        {
            // If an exception occurs, the connection is likely not successful
            return false;
        }
    }
    // Additional methods to interact with the third-party API
    public async Task<List<OrderDto>> GetAllOrdersAsync()
    { 
        bool isConnected = await IsCrmApiConnectedAsync();

        if (!isConnected)
        {
            throw new ApplicationException("CRM API is not available");
        }
        // Make an HTTP GET request to CRM API to fetch all orders
        HttpResponseMessage response = await _crmApiClient.GetAsync("orders");
        if(response.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }
        // Check if the request was successful
        response.EnsureSuccessStatusCode();

        // Parse the response and return the list of orders
        string responseContent = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<List<OrderDto>>(responseContent);

    }

    public async Task<OrderDto> GetOrderById(int id)
    {
        bool isConnected = await IsCrmApiConnectedAsync();

        if (!isConnected)
        {
            throw new ApplicationException("CRM API is not available");
        }
        HttpResponseMessage response = await _crmApiClient.GetAsync($"orders/{id}");
        if(response.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }
        string responseContent = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<OrderDto>(responseContent);
    }
}
