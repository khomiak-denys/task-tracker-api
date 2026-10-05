using DomainFramework.Results;
using Messaging.Abstractions;

namespace Workspaces.Application.Workspaces.Delete
{
    /// <summary>
    /// Command to delete a workspace. Only the owner or an admin may delete.
    /// </summary>
    /// <param name="WorkspaceId">The workspace to delete.</param>
    /// <param name="RequestingUserId">The id of the user making the request.</param>
    /// <param name="IsAdmin">Whether the requesting user has administrator privileges.</param>
    public record DeleteWorkspaceCommand(
        Guid WorkspaceId,
        Guid RequestingUserId,
        bool IsAdmin) : ICommand<Result>;
}
