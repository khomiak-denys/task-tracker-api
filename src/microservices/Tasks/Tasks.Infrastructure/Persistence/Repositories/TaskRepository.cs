using DomainFramework;
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
                    .ThenInclude(tt => tt.Tag)
                .Include(t => t.TimeLogs)
                .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<PaginationResult<TaskItem>> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken)
        {
            var query = _context.Tasks
                .Include(t => t.TaskTags)
                    .ThenInclude(tt => tt.Tag)
                .AsNoTracking()
                .OrderByDescending(t => t.CreatedAt);

            var totalCount = await query.CountAsync(cancellationToken);
            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return PaginationResult<TaskItem>.Create(items, page, pageSize, totalCount);
        }

        /// <inheritdoc/>
        public async Task<PaginationResult<TaskItem>> GetMyAsync(Guid userId, string? type, int page, int pageSize, CancellationToken cancellationToken)
        {
            var query = _context.Tasks
                .Include(t => t.TaskTags)
                    .ThenInclude(tt => tt.Tag)
                .AsNoTracking();

            if (string.Equals(type, "created", StringComparison.OrdinalIgnoreCase))
            {
                query = query.Where(t => t.CreatedById == userId);
            }
            else if (string.Equals(type, "assigned", StringComparison.OrdinalIgnoreCase))
            {
                query = query.Where(t => t.AssigneeId == userId);
            }
            else
            {
                query = query.Where(t => t.CreatedById == userId || t.AssigneeId == userId);
            }

            query = query.OrderByDescending(t => t.CreatedAt);

            var totalCount = await query.CountAsync(cancellationToken);
            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return PaginationResult<TaskItem>.Create(items, page, pageSize, totalCount);
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
