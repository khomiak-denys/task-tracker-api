using Workspaces.Application.Workspaces.Create;

namespace Workspaces.API.Workspaces.Requests
{
    /// <summary>
    /// HTTP request payload for creating a new workspace.
    /// </summary>
    /// <param name="Name">The workspace name.</param>
    /// <param name="Description">Optional description.</param>
    public record CreateWorkspaceRequest(string Name, string? Description = null)
    {
        /// <summary>
        /// Maps this request to a <see cref="CreateWorkspaceCommand"/>.
        /// </summary>
        /// <param name="ownerId">The authenticated user who will own the workspace.</param>
        /// <returns>A command instance.</returns>
        public CreateWorkspaceCommand ToCommand(Guid ownerId) =>
            new(Name, Description, ownerId);
    }
}
