using DomainFramework.Results;
using Messaging.Abstractions;
using Workspaces.Application.Workspaces.DTOs;

namespace Workspaces.Application.Workspaces.GetById
{
    /// <summary>
    /// Query to retrieve detailed information for a specific workspace.
    /// Guard: only the workspace owner or an admin can fetch details.
    /// </summary>
    /// <param name="WorkspaceId">The workspace identifier.</param>
    /// <param name="RequestingUserId">The user making the query.</param>
    /// <param name="IsAdmin">Whether the user has administrator privileges.</param>
    public record GetWorkspaceByIdQuery(
        Guid WorkspaceId,
        Guid RequestingUserId,
        bool IsAdmin) : IQuery<Result<WorkspaceDetailsResult>>;
}
