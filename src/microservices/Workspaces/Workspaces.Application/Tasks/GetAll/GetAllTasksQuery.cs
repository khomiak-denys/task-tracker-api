using DomainFramework;
using DomainFramework.Results;
using Messaging.Abstractions;
using Workspaces.Application.Tasks.DTOs;

namespace Workspaces.Application.Tasks.GetAll
{
    /// <summary>
    /// Query to retrieve a paged list of all tasks matching optional filter criteria.
    /// </summary>
    /// <param name="WorkspaceId">Optional workspace identifier filter.</param>
    /// <param name="Search">Optional search term matching task title or description.</param>
    /// <param name="Status">Optional status filter (e.g. 'Todo', 'InProgress', 'InReview', 'Done', 'Cancelled', or 'all').</param>
    /// <param name="Priority">Optional priority filter (e.g. 'Low', 'Medium', 'High', 'Critical', or 'all').</param>
    /// <param name="AssigneeId">Optional assignee identifier filter.</param>
    /// <param name="CreatedById">Optional creator identifier filter.</param>
    /// <param name="Tag">Optional tag name filter.</param>
    /// <param name="Type">Optional filter type ('created', 'assigned', 'all', or null).</param>
    /// <param name="RequestingUserId">Optional requesting user identifier used when filtering by type.</param>
    /// <param name="Page">The page number to retrieve.</param>
    /// <param name="PageSize">The number of items per page.</param>
    public record GetAllTasksQuery(
        Guid? WorkspaceId = null,
        string? Search = null,
        string? Status = null,
        string? Priority = null,
        Guid? AssigneeId = null,
        Guid? CreatedById = null,
        string? Tag = null,
        string? Type = null,
        Guid? RequestingUserId = null,
        int Page = 1,
        int PageSize = 10) : IQuery<Result<PaginationResult<TaskResult>>>;
}
