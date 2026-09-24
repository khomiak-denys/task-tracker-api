using Serilog;
using Microsoft.Extensions.Hosting;

namespace ServiceDefaults.Logging
{
    public static class SerilogExtensions
    {
        public static void AddSerilogLogging(this IHostBuilder builder)
        {
            builder.UseSerilog((context, services, configuration) => configuration
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services),
                preserveStaticLogger: false,
                writeToProviders: true);
        }
    }
}
