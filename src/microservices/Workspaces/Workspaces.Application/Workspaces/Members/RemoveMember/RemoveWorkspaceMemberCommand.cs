using DomainFramework.Results;
using Messaging.Abstractions;

namespace Workspaces.Application.Workspaces.Members.RemoveMember
{
    /// <summary>
    /// Command to remove a member from a workspace.
    /// Can be executed by the workspace owner, an admin, or the member leaving themselves.
    /// </summary>
    /// <param name="WorkspaceId">The workspace identifier.</param>
    /// <param name="MemberUserId">The user to remove from membership.</param>
    /// <param name="RequestingUserId">The user making the request.</param>
    /// <param name="IsAdmin">Whether the requesting user has administrator privileges.</param>
    public record RemoveWorkspaceMemberCommand(
        Guid WorkspaceId,
        Guid MemberUserId,
        Guid RequestingUserId,
        bool IsAdmin) : ICommand<Result>;
}
