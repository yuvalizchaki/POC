using POC.Contracts.CrmDTOs;
using Newtonsoft.Json;

namespace POC.Infrastructure.Adapters;

public class CrmAdapter
{
    
    // USE THIS CLASS BY INJECTING IT AS A DEPENDENCY FROM THE CONSTRUCTOR OF THE HANDLER:
    // EXAMPLE ->      public async Task<MyResult> Handle(MyCommand request, CancellationToken cancellationToken)
    // EXAMPLE ->      private readonly CrmAdapter _apiAdapter;
    // EXAMPLE ->     
    // EXAMPLE ->      public MyCommandHandler(CrmAdapter apiAdapter)
    // EXAMPLE ->      {
    // EXAMPLE ->          _apiAdapter = apiAdapter;
    // EXAMPLE ->      }
    // EXAMPLE ->     
    // EXAMPLE ->      public async Task<MyResult> Handle(MyCommand request, CancellationToken cancellationToken)
    // EXAMPLE ->      {
    // EXAMPLE ->          // Use the adapter to send requests
    // EXAMPLE ->          var apiResponse = await _apiAdapter.SendRequestAsync(apiRequest);
    // EXAMPLE ->     
    // EXAMPLE ->          // Process the response and return the result
    // EXAMPLE ->          return new MyResult();
    // EXAMPLE ->      }
    
    private readonly HttpClient _crmApiClient;
    //private readonly ILogger<CrmAdapter> _logger;

    public CrmAdapter(IHttpClientFactory httpClientFactory)
    {
        _crmApiClient = httpClientFactory.CreateClient("CrmApiClient");  
        _crmApiClient.BaseAddress = new Uri("http://localhost:8008/"); // Set the base address
    }

    public async Task<OrderDto> SendRequestAsync(OrderDto request)
    {
        // // Logic to send request to the third-party API
        // // and deserialize the response to ResponseType
        //
        // var response = await _httpClient.SendAsync(httpRequestMessage);
        // response.EnsureSuccessStatusCode();
        //
        // var content = await response.Content.ReadAsStringAsync();
        // return JsonConvert.DeserializeObject<OrderDto>(content);
        throw new NotImplementedException();
    }

    // Additional methods to interact with the third-party API
    public async Task<List<OrderDto>> GetAllOrdersAsync()
    { 
        // Make an HTTP GET request to CRM API to fetch all orders
        HttpResponseMessage response = await _crmApiClient.GetAsync("orders");

        // Check if the request was successful
        response.EnsureSuccessStatusCode();

        // Parse the response and return the list of orders
        string responseContent = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<List<OrderDto>>(responseContent);

    }
}
