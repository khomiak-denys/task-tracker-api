using DomainFramework;
using DomainFramework.Results;
using Messaging.Abstractions;
using Workspaces.Application.Tasks.DTOs;

namespace Workspaces.Application.Tasks.GetAll
{
    /// <summary>
    /// Query to retrieve a paged list of all tasks.
    /// </summary>
    /// <param name="Page">The page number to retrieve.</param>
    /// <param name="PageSize">The number of items per page.</param>
    public record GetAllTasksQuery(int Page = 1, int PageSize = 10) : IQuery<Result<PaginationResult<TaskResult>>>;
}
