using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ServiceDefaults.Authentification;
using ServiceDefaults.Authorization.IntegrationApiKey;

namespace ServiceDefaults.Authorization
{
    public static class AuthorizationExtensions
    {
        public static void AddAuthorization(this WebApplicationBuilder builder)
        {
            builder.Services
                .AddOptions<IntegrationApiKeyOptions>()
                .BindConfiguration(IntegrationApiKeyOptions.SectionName)
                .ValidateDataAnnotations()
                .ValidateOnStart();

            builder.Services.AddScoped<IAuthorizationHandler, IntegrationApiKeyHandler>();

            builder.Services.AddAuthorizationBuilder()
                .AddPolicy(Policies.IntegrationApiKey, policy =>
                {
                    policy.Requirements.Add(new IntegrationApiKeyRequirement());
                });
        }
    }
}
