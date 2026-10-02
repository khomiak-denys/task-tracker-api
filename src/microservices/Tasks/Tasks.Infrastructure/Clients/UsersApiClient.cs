using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using ServiceDefaults.Authorization.IntegrationApiKey;
using Tasks.Application.Abstractions;
using Tasks.Application.Tasks.DTOs;

namespace Tasks.Infrastructure.Clients
{
    /// <summary>
    /// HTTP client implementation for communicating with the Users microservice.
    /// </summary>
    public class UsersApiClient : IUsersApiClient
    {
        private const string ApiKeyHeader = "X-API-Key";
        private readonly HttpClient _httpClient;
        private readonly IOptions<IntegrationApiKeyOptions> _apiKeyOptions;

        public UsersApiClient(
            HttpClient httpClient,
            IOptions<IntegrationApiKeyOptions> apiKeyOptions)
        {
            _httpClient = httpClient;
            _apiKeyOptions = apiKeyOptions;
        }

        /// <inheritdoc />
        public async Task<UserResult?> GetByIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, $"api/v1/private-integrations/users/{userId}/contact-info");

            if (!string.IsNullOrWhiteSpace(_apiKeyOptions.Value.ApiKey))
            {
                request.Headers.Add(ApiKeyHeader, _apiKeyOptions.Value.ApiKey);
            }

            var response = await _httpClient.SendAsync(request, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            try
            {
                return await response.Content.ReadFromJsonAsync<UserResult>(cancellationToken);
            }
            catch
            {
                return null;
            }
        }
    }
}
