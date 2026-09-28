using Microsoft.EntityFrameworkCore;
using Tasks.Application.Abstractions;
using Tasks.Domain.Tags;
using Tasks.Domain.Tasks;

namespace Tasks.Infrastructure.Persistence
{
    public class TasksDbContext : DbContext, IUnitOfWork
    {
        public DbSet<TaskItem> Tasks => Set<TaskItem>();

        public DbSet<Tag> Tags => Set<Tag>();
        public DbSet<TaskTag> TaskTags => Set<TaskTag>();
        public DbSet<TimeLog> TimeLogs => Set<TimeLog>();

        public TasksDbContext(DbContextOptions<TasksDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TasksDbContext).Assembly);
        }
    }
}
