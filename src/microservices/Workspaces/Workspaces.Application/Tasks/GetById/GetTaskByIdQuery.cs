using DomainFramework.Results;
using Messaging.Abstractions;
using Workspaces.Application.Tasks.DTOs;

namespace Workspaces.Application.Tasks.GetById
{
    /// <summary>
    /// Query to retrieve detailed information about a specific task.
    /// </summary>
    /// <param name="TaskId">The id of the task to retrieve.</param>
    /// <param name="RequestingUserId">The id of the user making the request.</param>
    /// <param name="IsAdmin">Whether the requesting user has administrator privileges.</param>
    public record GetTaskByIdQuery(
        Guid TaskId,
        Guid RequestingUserId,
        bool IsAdmin) : IQuery<Result<TaskDetailsResult>>;
}
