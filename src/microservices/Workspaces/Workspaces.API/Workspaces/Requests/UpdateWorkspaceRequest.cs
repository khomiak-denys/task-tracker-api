using Workspaces.Application.Workspaces.Update;

namespace Workspaces.API.Workspaces.Requests
{
    /// <summary>
    /// HTTP request payload for updating an existing workspace.
    /// </summary>
    /// <param name="Name">The updated workspace name.</param>
    /// <param name="Description">The updated workspace description.</param>
    public record UpdateWorkspaceRequest(string Name, string? Description = null)
    {
        /// <summary>
        /// Maps this request to an <see cref="UpdateWorkspaceCommand"/>.
        /// </summary>
        /// <param name="workspaceId">The workspace identifier.</param>
        /// <param name="requestingUserId">The current user identifier.</param>
        /// <param name="isAdmin">Whether the user is an administrator.</param>
        /// <returns>A command instance.</returns>
        public UpdateWorkspaceCommand ToCommand(Guid workspaceId, Guid requestingUserId, bool isAdmin) =>
            new(workspaceId, requestingUserId, isAdmin, Name, Description);
    }
}
