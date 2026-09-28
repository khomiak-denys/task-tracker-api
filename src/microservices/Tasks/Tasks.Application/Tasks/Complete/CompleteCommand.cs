using DomainFramework.Results;
using Messaging.Abstractions;

namespace Tasks.Application.Tasks.Complete
{
    /// <summary>
    /// Command to mark a task as completed (<c>Done</c>).
    /// Allowed only when the task is currently in <c>InReview</c> status.
    /// </summary>
    /// <param name="TaskId">The task to complete.</param>
    /// <param name="RequestedById">Id of the authenticated user completing the task.</param>
    public record CompleteCommand(
        Guid TaskId,
        Guid RequestedById) : ICommand<Result>;
}
