using Workspaces.Application.Tasks.GetAll;

namespace Workspaces.API.Tasks.Requests
{
    /// <summary>
    /// Request model for querying all tasks with optional filters and pagination.
    /// </summary>
    /// <param name="WorkspaceId">Optional workspace identifier filter.</param>
    /// <param name="Search">Optional search term matching task title or description.</param>
    /// <param name="Status">Optional task status filter.</param>
    /// <param name="Priority">Optional task priority filter.</param>
    /// <param name="AssigneeId">Optional assignee identifier filter.</param>
    /// <param name="CreatedById">Optional creator identifier filter.</param>
    /// <param name="Tag">Optional tag name filter.</param>
    /// <param name="Type">Optional filter type ('created', 'assigned', 'all', or null).</param>
    /// <param name="Page">The page number to retrieve (default is 1).</param>
    /// <param name="PageSize">The number of items per page (default is 10).</param>
    public record GetAllTasksRequest(
        Guid? WorkspaceId = null,
        string? Search = null,
        string? Status = null,
        string? Priority = null,
        Guid? AssigneeId = null,
        Guid? CreatedById = null,
        string? Tag = null,
        string? Type = null,
        int Page = 1,
        int PageSize = 10)
    {
        /// <summary>
        /// Converts the request to a <see cref="GetAllTasksQuery"/>.
        /// </summary>
        /// <param name="currentUserId">The requesting user identifier.</param>
        /// <returns>A new <see cref="GetAllTasksQuery"/> instance.</returns>
        public GetAllTasksQuery ToQuery(Guid? currentUserId)
        {
            return new GetAllTasksQuery(
                WorkspaceId,
                Search,
                Status,
                Priority,
                AssigneeId,
                CreatedById,
                Tag,
                Type,
                currentUserId,
                Page,
                PageSize);
        }
    }
}
