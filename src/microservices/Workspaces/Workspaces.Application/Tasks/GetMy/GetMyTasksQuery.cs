using DomainFramework;
using DomainFramework.Results;
using Messaging.Abstractions;
using Workspaces.Application.Tasks.DTOs;

namespace Workspaces.Application.Tasks.GetMy
{
    /// <summary>
    /// Query to retrieve tasks assigned to or created by the requesting user.
    /// </summary>
    /// <param name="UserId">The id of the user.</param>
    /// <param name="Type">The filter type ('created', 'assigned', or null for all).</param>
    /// <param name="Page">The page number.</param>
    /// <param name="PageSize">The page size.</param>
    public record GetMyTasksQuery(
        Guid UserId,
        string? Type,
        int Page = 1,
        int PageSize = 10) : IQuery<Result<PaginationResult<TaskResult>>>;
}
