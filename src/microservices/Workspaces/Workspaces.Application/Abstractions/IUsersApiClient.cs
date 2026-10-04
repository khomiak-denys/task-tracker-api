using Workspaces.Application.Tasks.DTOs;

namespace Workspaces.Application.Abstractions
{
    /// <summary>
    /// HTTP client abstraction for communicating with the Users microservice.
    /// </summary>
    public interface IUsersApiClient
    {
        /// <summary>
        /// Retrieves user details by identifier from the Users API.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The user details if found; otherwise, <c>null</c>.</returns>
        Task<UserResult?> GetByIdAsync(Guid userId, CancellationToken cancellationToken);
    }
}
