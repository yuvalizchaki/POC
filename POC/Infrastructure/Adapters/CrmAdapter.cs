using POC.Contracts.CrmDTOs;

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
    
    private readonly HttpClient _httpClient;

    public CrmAdapter(HttpClient httpClient)
    {
        _httpClient = httpClient;
        // Configure httpClient if necessary (e.g., BaseAddress, Default Headers)
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
}
