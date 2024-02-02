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
    // Additional methods to interact with the third-party API
    public async Task<List<OrderDto>> GetAllOrdersAsync()
    { 
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
        
        HttpResponseMessage response = await _crmApiClient.GetAsync($"orders/{id}");
        if(response.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }
        string responseContent = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<OrderDto>(responseContent);
    }
}
