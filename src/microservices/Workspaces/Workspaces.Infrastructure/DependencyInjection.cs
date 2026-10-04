using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Workspaces.Application.Abstractions;
using Workspaces.Domain.Tags;
using Workspaces.Domain.Tasks;
using Workspaces.Infrastructure.Clients;
using Workspaces.Infrastructure.Persistence;
using Workspaces.Infrastructure.Persistence.Repositories;

namespace Workspaces.Infrastructure
{
    /// <summary>
    /// Extension methods for registering infrastructure services in the dependency injection container.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Registers infrastructure layer services, including EF Core DbContext, repositories, and HTTP clients.
        /// </summary>
        /// <param name="services">The service collection to register into.</param>
        /// <param name="configuration">The application configuration root.</param>
        /// <returns>The modified service collection.</returns>
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddHttpContextAccessor();

            services.AddDbContext<WorkspacesDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("WorkspacesDb") ?? configuration.GetConnectionString("TasksDb")));

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<WorkspacesDbContext>());
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<ITagRepository, TagRepository>();

            services.AddHttpClient<IUsersApiClient, UsersApiClient>(client =>
            {
                client.BaseAddress = new Uri(configuration["Microservices:UsersApi"]
                    ?? throw new InvalidOperationException("UsersApi URL is not configured."));
            });

            return services;
        }
    }
}
