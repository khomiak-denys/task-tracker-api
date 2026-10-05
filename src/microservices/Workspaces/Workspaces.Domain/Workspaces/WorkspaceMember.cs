using DomainFramework;

namespace Workspaces.Domain.Workspaces
{
    /// <summary>
    /// Join entity representing a user's membership in a <see cref="Workspace"/>.
    /// </summary>
    public class WorkspaceMember : EntityBase
    {
        /// <summary>Gets the workspace identifier.</summary>
        public Guid WorkspaceId { get; private set; }

        /// <summary>Gets the member's user identifier.</summary>
        public Guid UserId { get; private set; }

        /// <summary>Navigation property back to the owning workspace.</summary>
        public Workspace Workspace { get; private set; } = null!;

        /// <summary>EF Core parameterless constructor.</summary>
        private WorkspaceMember() { }

        private WorkspaceMember(Guid workspaceId, Guid userId)
        {
            WorkspaceId = workspaceId;
            UserId = userId;
        }

        /// <summary>
        /// Factory method that creates a new <see cref="WorkspaceMember"/>.
        /// </summary>
        /// <param name="workspaceId">The workspace to join.</param>
        /// <param name="userId">The user being added.</param>
        /// <returns>A new workspace member entity.</returns>
        public static WorkspaceMember Create(Guid workspaceId, Guid userId)
        {
            var member = new WorkspaceMember(workspaceId, userId);
            member.OnCreate();
            return member;
        }
    }
}
