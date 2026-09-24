using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MigrationsExtensions
{
    public static class EfCoreMigrationsExtensions
    {
        public static void ApplyMigrations<TDbContext>(this WebApplication app) where TDbContext : DbContext
        {
            using var scope = app.Services.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<TDbContext>();
            dbContext.Database.Migrate();
        }

        public static async Task ApplyMigrationsAsync<TDbContext>(this WebApplication app, CancellationToken cancellationToken = default) where TDbContext : DbContext
        {
            await using var scope = app.Services.CreateAsyncScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<TDbContext>();
            await dbContext.Database.MigrateAsync(cancellationToken);
        }
    }
}
