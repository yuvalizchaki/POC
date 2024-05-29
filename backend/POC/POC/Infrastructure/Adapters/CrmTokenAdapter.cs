using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using POC.Contracts.CrmDTOs;
using POC.Infrastructure.Common;
using POC.Infrastructure.Common.Exceptions;

namespace POC.Infrastructure.Adapters
{
    // TODO: Implement CRM adapter and related classes
    public class CrmTokenAdapter
    {
        private const string CRM_TOKEN_URL = "connect/token";
        private readonly HttpClient _httpClient;
        private readonly ILogger<CrmTokenAdapter> _logger;
        private readonly string? _clientId;
        private readonly string? _clientSecret;
        private string? _authToken;
        private readonly JsonSerializerOptions _crmAuthJsonSerializerOptions;
        private string? _accessToken;
        private readonly string _authBaseUrl;

        public CrmTokenAdapter(IHttpClientFactory httpClientFactory, ILogger<CrmTokenAdapter> logger, IConfiguration configuration)
        {
            _authBaseUrl = configuration.GetValue<string>("ToyCrm_AuthBaseUrl") ?? throw new CrmAdapterError("Could not find configuration ToyCrm_AuthBaseUrl");
            _httpClient = httpClientFactory.CreateClient("CrmApiClient");
            _logger = logger;
            _clientId = configuration.GetValue<string>("ToyCrm_ClientId") ?? throw new CrmAdapterError("Could not find configuration ToyCrm_ClientId");
            _clientSecret = configuration.GetValue<string>("ToyCrm_ClientSecret") ?? throw new CrmAdapterError("Could not find configuration ToyCrm_ClientSecret");
            _authToken = configuration.GetValue<string>("ToyCrm_AuthToken") ?? throw new CrmAdapterError("Could not find configuration ToyCrm_AuthToken");
            _crmAuthJsonSerializerOptions = CrmAuthJsonOptionsConfigurator.GetConfiguredOptions();
        }

        public async Task<string> GetTokenAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, _authBaseUrl + CRM_TOKEN_URL);
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", _authToken);
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string?>("grant_type", "client_credentials"),
                new KeyValuePair<string, string?>("client_id", _clientId),
                new KeyValuePair<string, string?>("client_secret", _clientSecret)
            });

            request.Content = content;
            _httpClient.BaseAddress = new Uri(_authBaseUrl);
            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            // _logger.LogInformation("[DEBUG] CRM response content: {ResponseContent}", responseContent);

            var tokenResponse = JsonSerializer.Deserialize<TokenResponseDto>(responseContent, _crmAuthJsonSerializerOptions) ?? throw new CrmAdapterError("Failed to get access token.");

            _accessToken = tokenResponse.AccessToken;

            return _accessToken;
        }
    }
}
