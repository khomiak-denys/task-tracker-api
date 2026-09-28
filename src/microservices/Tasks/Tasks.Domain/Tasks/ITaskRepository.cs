namespace Tasks.Domain.Tasks
{
    /// <summary>
    /// Persistence contract for the <see cref="TaskItem"/> aggregate.
    /// Implemented in <c>Tasks.Infrastructure</c>.
    /// </summary>
    public interface ITaskRepository
    {
        /// <summary>Returns a <see cref="TaskItem"/> by its id, including tags and time logs.</summary>
        /// <param name="id">Task identifier.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The task, or <c>null</c> if not found.</returns>
        Task<TaskItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

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
