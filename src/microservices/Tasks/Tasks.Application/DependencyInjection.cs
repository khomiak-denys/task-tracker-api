using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Tasks.Application
{
    /// <summary>
    /// Extension methods that register all Application-layer services into the DI container.
    /// Called from the API composition root (<c>Tasks.API/Program.cs</c>).
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Registers MediatR handlers, pipeline behaviors, and FluentValidation validators
        /// from the <c>Tasks.Application</c> assembly.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The same <paramref name="services"/> for chaining.</returns>
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

            return services;
        }
    }
}
