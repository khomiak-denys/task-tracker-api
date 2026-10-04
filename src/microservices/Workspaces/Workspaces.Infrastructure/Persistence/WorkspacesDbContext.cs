using Microsoft.EntityFrameworkCore;
using Workspaces.Application.Abstractions;
using Workspaces.Domain.Tags;
using Workspaces.Domain.Tasks;

namespace Workspaces.Infrastructure.Persistence
{
    public class WorkspacesDbContext : DbContext, IUnitOfWork
    {
        public DbSet<TaskItem> Tasks => Set<TaskItem>();

        public DbSet<Tag> Tags => Set<Tag>();
        public DbSet<TaskTag> TaskTags => Set<TaskTag>();
        public DbSet<TimeLog> TimeLogs => Set<TimeLog>();

        public WorkspacesDbContext(DbContextOptions<WorkspacesDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WorkspacesDbContext).Assembly);
        }
    }
}
