using Microsoft.EntityFrameworkCore;
using Tasks.Domain.Tasks;

namespace Tasks.Infrastructure.Persistence.Repositories
{
    /// <summary>
    /// EF Core implementation of <see cref="ITaskRepository"/>.
    /// </summary>
    internal sealed class TaskRepository : ITaskRepository
    {
        private readonly TasksDbContext _context;

        public TaskRepository(TasksDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public async Task<TaskItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Tasks
                .Include(t => t.TaskTags)
                .Include(t => t.TimeLogs)
                .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task AddAsync(TaskItem task, CancellationToken cancellationToken)
        {
            await _context.Tasks.AddAsync(task, cancellationToken);
        }

        /// <inheritdoc/>
        public Task RemoveAsync(TaskItem task, CancellationToken cancellationToken)
        {
            _context.Tasks.Remove(task);
            return Task.CompletedTask;
        }
    }
}
