using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Users.Infrastructure.Identity;

namespace Users.Infrastructure.Persistence
{
    public class UsersDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(UsersDbContext).Assembly);
        }
    }
}
