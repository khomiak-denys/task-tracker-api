using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tasks.Application.Abstractions;
using Tasks.Domain.Tags;
using Tasks.Domain.Tasks;
using Tasks.Infrastructure.Clients;
using Tasks.Infrastructure.Persistence;
using Tasks.Infrastructure.Persistence.Repositories;

namespace Tasks.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<TasksDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("TasksDb")));

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<TasksDbContext>());
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<ITagRepository, TagRepository>();

            services.AddHttpClient<IUsersApiClient, UsersApiClient>(client =>
            {
                var baseUrl = configuration["UsersApi:BaseUrl"];
                if (!string.IsNullOrWhiteSpace(baseUrl))
                {
                    client.BaseAddress = new Uri(baseUrl);
                }
            });

            return services;
        }
    }
}
