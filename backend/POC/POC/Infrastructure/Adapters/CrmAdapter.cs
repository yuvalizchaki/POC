using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using POC.Contracts.CrmDTOs;
using POC.Infrastructure.Common;
using POC.Infrastructure.Common.Exceptions;
using POC.Infrastructure.Models.CrmSearchQuery;

namespace POC.Infrastructure.Adapters
{
    // TODO: Implement CRM adapter and related classes
    public class CrmAdapter : IOrderAdapter, ITypesAdapter
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly CrmTokenAdapter _tokenAdapter;
        private readonly ILogger<CrmAdapter> _logger;
        private readonly JsonSerializerOptions _crmApiJsonSerializerOptions;
        private readonly string _apiBaseUrl;

        public CrmAdapter(IHttpClientFactory httpClientFactory, CrmTokenAdapter tokenAdapter, ILogger<CrmAdapter> logger, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _tokenAdapter = tokenAdapter;
            _logger = logger;
            _apiBaseUrl = configuration.GetValue<string>("ToyCrm_ApiBaseUrl") ?? throw new CrmAdapterError("Could not find configuration ToyCrm_ApiBaseUrl");
            _crmApiJsonSerializerOptions = CrmApiJsonOptionsConfigurator.GetConfiguredOptions();
        }

        private async Task<HttpClient> GetAuthenticatedClientAsync()
        {
            var token = await _tokenAdapter.GetTokenAsync();
            var client = _httpClientFactory.CreateClient("CrmApiClient");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            if (client.BaseAddress == null)
            {
                client.BaseAddress = new Uri(_apiBaseUrl);
            }
            return client;
        }

        private async Task<string> SendRequestAsync(HttpMethod method, string url, object content = null)
        {
            using var client = await GetAuthenticatedClientAsync();
            var request = new HttpRequestMessage(method, _apiBaseUrl + url)
            {
                Content = content != null ? new StringContent(JsonSerializer.Serialize(content, _crmApiJsonSerializerOptions), Encoding.UTF8, "application/json") : null
            };
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _logger.LogInformation("[DEBUG] Request: {Method} {Uri} - Headers: {Headers} - Content: {Content}",
                request.Method, request.RequestUri, string.Join(", ", request.Headers.Select(h => $"{h.Key}: {string.Join(", ", h.Value)}")),
                content != null ? JsonSerializer.Serialize(content, _crmApiJsonSerializerOptions) : "No Content");

            var response = await client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.NotFound)
                throw new HttpRequestException($"Resource not found at {_apiBaseUrl}{url}");

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        private async Task<T> SendRequestAsync<T>(HttpMethod method, string url, object content = null)
        {
            var responseContent = await SendRequestAsync(method, url, content);
            return JsonSerializer.Deserialize<T>(responseContent, _crmApiJsonSerializerOptions) ?? throw new CrmAdapterError("Failed to deserialize CRM response.");
        }

        public Task<List<OrderDto>> FetchOrdersAsync(int companyId = 1)
        {
            var url = $"orders/{companyId}/search";
            var request = SearchRequestBuilder.Empty.Build();
            var response = SendRequestAsync<OrderQueryResponse>(HttpMethod.Post, url, request).Result.Items;
            return Task.FromResult(response);
        }

        public Task<string> FetchTagsTypesAsync(int companyId = 1)
        {
            var url = $"orders/{companyId}/tags";
            return SendRequestAsync(HttpMethod.Get, url);
        }

        public Task<string> FetchCompanyTypesAsync(int companyId = 1)
        {
            var url = $"units/{companyId}/departments";
            return SendRequestAsync(HttpMethod.Get, url);
        }
    }
}
