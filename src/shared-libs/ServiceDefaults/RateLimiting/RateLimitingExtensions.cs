using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

namespace ServiceDefaults.RateLimiting
{
    public static class RateLimitingExtensions
    {
        public static void AddRateLimiting(this WebApplicationBuilder builder)
        {
            builder.Services.AddRateLimiter(options =>
            {
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

                options.AddTokenBucketLimiter("public-api", opt =>
                {
                    opt.TokenLimit = 100;
                    opt.TokensPerPeriod = 100;
                    opt.ReplenishmentPeriod = TimeSpan.FromSeconds(60);
                    opt.AutoReplenishment = true;
                    opt.QueueLimit = 0;
                    opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                });
            });
        }
    }
}
