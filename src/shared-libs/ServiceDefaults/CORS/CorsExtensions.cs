using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceDefaults.CORS
{
    public static class CorsExtensions
    {
        public static void AddCors(this WebApplicationBuilder builder, CorsOptions corsOptions)
        {
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(corsOptions.Name, policy =>
                {
                    policy.WithOrigins(corsOptions.AllowedOrigins)
                          .WithMethods(corsOptions.AllowedMethods)
                          .WithHeaders(corsOptions.AllowedHeaders)
                          .AllowCredentials();
                });
            });
        }
    }
}
