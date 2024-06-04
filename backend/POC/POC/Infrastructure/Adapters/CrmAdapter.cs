using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using POC.Contracts.CrmDTOs;
using POC.Infrastructure.Common;
using POC.Infrastructure.Common.Exceptions;
using POC.Infrastructure.Models.CrmSearchQuery;

namespace POC.Infrastructure.Adapters
{
    // TODO: Implement CRM adapter and related classes
    public class CrmAdapter
    {
        private readonly HttpClient _httpClient;
        private readonly CrmTokenAdapter _tokenAdapter;
        private readonly ILogger<CrmAdapter> _logger;
        private readonly JsonSerializerOptions _crmApiJsonSerializerOptions;
        private readonly string _apiBaseUrl;

        public CrmAdapter(IHttpClientFactory httpClientFactory, CrmTokenAdapter tokenAdapter, ILogger<CrmAdapter> logger, IConfiguration configuration)
        {
            _apiBaseUrl = configuration.GetValue<string>("ToyCrm_ApiBaseUrl") ?? throw new CrmAdapterError("Could not find configuration ToyCrm_ApiBaseUrl");
            _httpClient = httpClientFactory.CreateClient("CrmApiClient");
            _tokenAdapter = tokenAdapter;
            _logger = logger;
            _crmApiJsonSerializerOptions = CrmApiJsonOptionsConfigurator.GetConfiguredOptions();
        }

        private async Task AddAuthenticationHeaderAsync()
        {
            var token = await _tokenAdapter.GetTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<List<OrderDto>> GetAllOrdersAsync(int companyId, SearchRequest queryContent)
        {
            await AddAuthenticationHeaderAsync();

            string crmOrdersUrl = getCrmOrderUrlFromCompany(companyId);

            var request = new HttpRequestMessage(HttpMethod.Post, _apiBaseUrl + crmOrdersUrl);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Content = new StringContent(JsonSerializer.Serialize(queryContent, _crmApiJsonSerializerOptions), Encoding.UTF8, "application/json");
             _logger.LogInformation("[DEBUG] Request: {Method} {Uri} - Headers: {Headers} - Content: {Content}", request.Method, request.RequestUri, string.Join(", ", request.Headers.Select(h => $"{h.Key}: {string.Join(", ", h.Value)}")), await request.Content.ReadAsStringAsync());
            _httpClient.BaseAddress = new Uri(_apiBaseUrl);
            var response = await _httpClient.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.NotFound)
                throw new HttpRequestException($"Resource not found at {_apiBaseUrl}{crmOrdersUrl}");


            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();

            // _logger.LogInformation("[DEBUG] CRM response content: {ResponseContent}", responseContent);

            var jsonResponseContent = JsonSerializer.Deserialize<OrderQueryResponse>(responseContent, _crmApiJsonSerializerOptions) ?? throw new CrmAdapterError("Failed to deserialize crm response.");
            return jsonResponseContent.Items;
        }

        private string getCrmOrderUrlFromCompany(int companyId)
        {
            return "orders/" + companyId + "/search";
        }
    }
}
