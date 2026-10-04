using Workspaces.Application.Tasks.LogTime;

namespace Workspaces.API.Tasks.Requests
{
    public record LogTaskTimeRequest(
        int MinutesSpent,
        string? Description,
        DateOnly LoggedDate)
    {
        public LogTaskTimeCommand ToCommand(Guid taskId, Guid userId)
        {
            return new LogTaskTimeCommand(taskId, userId, MinutesSpent, Description, LoggedDate);
        }
    }
}
