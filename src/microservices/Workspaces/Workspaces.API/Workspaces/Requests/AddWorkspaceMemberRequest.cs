using Workspaces.Application.Workspaces.Members.AddMember;

namespace Workspaces.API.Workspaces.Requests
{
    /// <summary>
    /// HTTP request payload for adding a member to a workspace.
    /// </summary>
    /// <param name="UserId">The user identifier to add as a member.</param>
    public record AddWorkspaceMemberRequest(Guid UserId)
    {
        /// <summary>
        /// Maps this request to an <see cref="AddWorkspaceMemberCommand"/>.
        /// </summary>
        /// <param name="workspaceId">The workspace identifier.</param>
        /// <param name="requestingUserId">The current user identifier.</param>
        /// <param name="isAdmin">Whether the user is an administrator.</param>
        /// <returns>A command instance.</returns>
        public AddWorkspaceMemberCommand ToCommand(Guid workspaceId, Guid requestingUserId, bool isAdmin) =>
            new(workspaceId, UserId, requestingUserId, isAdmin);
    }
}
