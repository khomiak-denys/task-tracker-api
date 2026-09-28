namespace Tasks.Application.Abstractions
{
    /// <summary>
    /// HTTP client abstraction for communicating with the Users microservice.
    /// </summary>
    public interface IUsersApiClient
    {
        /// <summary>
        /// Checks if a user with the specified identifier exists by calling the Users API.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns><c>true</c> if the user exists; otherwise, <c>false</c>.</returns>
        Task<bool> ExistsAsync(Guid userId, CancellationToken cancellationToken);
    }
}
