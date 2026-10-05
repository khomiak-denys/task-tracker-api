using DomainFramework.Results;
using Messaging.Abstractions;

namespace Workspaces.Application.Tasks.LogTime
{
    /// <summary>
    /// Command to log time spent on a task.
    /// </summary>
    /// <param name="TaskId">Id of the task.</param>
    /// <param name="UserId">Id of the user logging the time.</param>
    /// <param name="MinutesSpent">Positive number of minutes spent.</param>
    /// <param name="Description">Optional note describing what was done.</param>
    /// <param name="LoggedDate">The calendar date on which the work was performed.</param>
    public record LogTaskTimeCommand(
        Guid TaskId,
        Guid UserId,
        int MinutesSpent,
        string? Description,
        DateOnly LoggedDate) : ICommand<Result>;
}
