namespace Tasks.Domain.Tags
{
    /// <summary>
    /// Persistence contract for the <see cref="Tag"/> entity.
    /// Implemented in <c>Tasks.Infrastructure</c>.
    /// </summary>
    public interface ITagRepository
    {
        /// <summary>Returns a tag by its name, or <c>null</c> if it does not exist.</summary>
        /// <param name="name">Tag name (case-insensitive lookup recommended).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The matching <see cref="Tag"/>, or <c>null</c>.</returns>
        Task<Tag?> GetByNameAsync(string name, CancellationToken cancellationToken);

        /// <summary>Adds a new <see cref="Tag"/> to the persistence context asynchronously.</summary>
        /// <param name="tag">The tag to add.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task AddAsync(Tag tag, CancellationToken cancellationToken);
    }
}
