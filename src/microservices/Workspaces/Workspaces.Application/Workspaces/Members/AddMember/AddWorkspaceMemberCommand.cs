using DomainFramework.Results;
using Messaging.Abstractions;

namespace Workspaces.Application.Workspaces.Members.AddMember
{
    /// <summary>
    /// Command to add a member to a workspace. Only the owner or an admin can add members.
    /// </summary>
    /// <param name="WorkspaceId">The workspace identifier.</param>
    /// <param name="MemberUserId">The user to add as a member.</param>
    /// <param name="RequestingUserId">The user making the request.</param>
    /// <param name="IsAdmin">Whether the requesting user has administrator privileges.</param>
    public record AddWorkspaceMemberCommand(
        Guid WorkspaceId,
        Guid MemberUserId,
        Guid RequestingUserId,
        bool IsAdmin) : ICommand<Result>;
}
