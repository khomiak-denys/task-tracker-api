using DomainFramework.Results;
using Messaging.Abstractions;

namespace Workspaces.Application.Workspaces.Update
{
    /// <summary>
    /// Command to update an existing workspace's name and description.
    /// </summary>
    /// <param name="WorkspaceId">The workspace to update.</param>
    /// <param name="RequestingUserId">The id of the user making the request.</param>
    /// <param name="IsAdmin">Whether the requesting user has administrator privileges.</param>
    /// <param name="Name">Updated name (max 200 chars).</param>
    /// <param name="Description">Updated description (max 2000 chars).</param>
    public record UpdateWorkspaceCommand(
        Guid WorkspaceId,
        Guid RequestingUserId,
        bool IsAdmin,
        string Name,
        string? Description) : ICommand<Result>;
}
