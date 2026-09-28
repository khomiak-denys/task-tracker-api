using Tasks.Application.Tasks.LogTime;

namespace Tasks.API.Tasks.Requests
{
    public record LogTimeRequest(
        int MinutesSpent,
        string? Description,
        DateOnly LoggedDate)
    {
        public LogTimeCommand ToCommand(Guid taskId, Guid userId)
        {
            return new LogTimeCommand(taskId, userId, MinutesSpent, Description, LoggedDate);
        }
    }
}
