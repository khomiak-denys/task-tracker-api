using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ServiceDefaults.ErrorHandling.ExpectionMapper;

namespace ServiceDefaults.ErrorHandling
{
    public static class WebApplicationBuilderExtensions
    {
        public static void AddErrorHandling(this WebApplicationBuilder builder)
        {
            builder.Services.AddProblemDetails();
            builder.Services.AddSingleton<IExceptionProblemDetailsMapper, ExceptionProblemDetailsMapper>();
            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        }
    }
}
