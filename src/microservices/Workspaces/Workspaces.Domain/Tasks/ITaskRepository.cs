using DomainFramework;

namespace Workspaces.Domain.Tasks
{
    /// <summary>
    /// Persistence contract for the <see cref="TaskItem"/> aggregate.
    /// Implemented in <c>Workspaces.Infrastructure</c>.
    /// </summary>
    public interface ITaskRepository
    {
        /// <summary>Returns a <see cref="TaskItem"/> by its id, including tags and time logs.</summary>
        /// <param name="id">Task identifier.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The task, or <c>null</c> if not found.</returns>
        Task<TaskItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>Returns a paginated list of all tasks.</summary>
        /// <param name="page">Page number (1-based).</param>
        /// <param name="pageSize">Page size.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Paginated result of tasks.</returns>
        Task<PaginationResult<TaskItem>> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken);

        /// <summary>Returns a paginated list of tasks filtered by creator, assignee, or both.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="type">Filter type: "created", "assigned", or null/empty for both.</param>
        /// <param name="page">Page number (1-based).</param>
        /// <param name="pageSize">Page size.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Paginated result of tasks.</returns>
        Task<PaginationResult<TaskItem>> GetMyAsync(Guid userId, string? type, int page, int pageSize, CancellationToken cancellationToken);

        /// <summary>Adds a new <see cref="TaskItem"/> to the persistence context asynchronously.</summary>
        /// <param name="task">The task to add.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task AddAsync(TaskItem task, CancellationToken cancellationToken);

        /// <summary>Removes a <see cref="TaskItem"/> from the persistence context asynchronously.</summary>
        /// <param name="task">The task to remove.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task RemoveAsync(TaskItem task, CancellationToken cancellationToken);
    }
}
