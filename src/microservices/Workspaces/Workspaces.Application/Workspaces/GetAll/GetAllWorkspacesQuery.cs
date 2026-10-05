using DomainFramework;
using DomainFramework.Results;
using Messaging.Abstractions;
using Workspaces.Application.Workspaces.DTOs;

namespace Workspaces.Application.Workspaces.GetAll
{
    /// <summary>
    /// Query to retrieve a paginated list of workspaces.
    /// Admins receive all workspaces; non-admins receive workspaces they belong to as a member.
    /// </summary>
    /// <param name="RequestingUserId">The user making the query.</param>
    /// <param name="IsAdmin">Whether the user has administrator privileges.</param>
    /// <param name="Name">Optional workspace name filter.</param>
    /// <param name="Page">The 1-based page number.</param>
    /// <param name="PageSize">The page size.</param>
    public record GetAllWorkspacesQuery(
        Guid RequestingUserId,
        bool IsAdmin,
        string? Name = null,
        int Page = 1,
        int PageSize = 10) : IQuery<Result<PaginationResult<WorkspaceResult>>>;
}
