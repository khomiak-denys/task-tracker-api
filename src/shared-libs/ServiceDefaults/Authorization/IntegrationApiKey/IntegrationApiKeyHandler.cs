using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ServiceDefaults.Authorization.IntegrationApiKey
{
    public partial class IntegrationApiKeyHandler : AuthorizationHandler<IntegrationApiKeyRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOptions<IntegrationApiKeyOptions> _options;
        private readonly ILogger<IntegrationApiKeyHandler> _logger;
        private const string ApiKeyHeader = "X-API-Key";

        public IntegrationApiKeyHandler(IHttpContextAccessor httpContextAccessor, IOptions<IntegrationApiKeyOptions> options, ILogger<IntegrationApiKeyHandler> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _options = options;
            _logger = logger;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IntegrationApiKeyRequirement requirement)
        {
            if (_httpContextAccessor.HttpContext!.Request.Headers.TryGetValue(ApiKeyHeader, out var authorizationHeader))
            {
                var token = authorizationHeader.ToString().Replace(ApiKeyHeader, "", StringComparison.OrdinalIgnoreCase).Trim();

                if (String.Equals(_options.Value.ApiKey, token, StringComparison.OrdinalIgnoreCase))
                {
                    context.Succeed(requirement);
                }
            }

            else context.Fail();

            return Task.CompletedTask;
        }

        public override string ToString()
        {
            const string errorMessage = "User is not verified.";
            return $"{nameof(IntegrationApiKeyRequirement)}: {errorMessage}";
        }
    }
}
