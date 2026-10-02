using DomainFramework.Results;
using Messaging.Abstractions;

namespace Tasks.Application.Tasks.Assign
{
    /// <summary>
    /// Command to assign a task to a user.
    /// </summary>
    /// <param name="TaskId">The id of the task to assign.</param>
    /// <param name="AssigneeId">The id of the user to assign the task to.</param>
    /// <param name="RequestedById">The id of the user requesting the assignment.</param>
    public record AssignTaskCommand(
        Guid TaskId,
        Guid AssigneeId,
        Guid RequestedById) : ICommand<Result>;
}
