namespace Workspaces.Application.Abstractions
{
    /// <summary>
    /// Abstracts the database transaction boundary.
    /// Implemented by the EF Core DbContext in <c>Workspaces.Infrastructure</c>.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>Persists all pending changes to the database.</summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Number of state entries written.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
