using DomainFramework;

namespace Workspaces.Domain.Tasks
{
    public class TimeLog : EntityBase
    {
        public Guid TaskId { get; private set; }

        public Guid UserId { get; private set; }

        public int MinutesSpent { get; private set; }

        public string? Description { get; private set; }

        public DateOnly LoggedDate { get; private set; }

        public TaskItem Task { get; private set; } = null!;

        private TimeLog() { }

        private TimeLog(Guid taskId, Guid userId, int minutesSpent, string? description, DateOnly loggedDate)
        {
            TaskId = taskId;
            UserId = userId;
            MinutesSpent = minutesSpent;
            Description = description;
            LoggedDate = loggedDate;
        }

        public static TimeLog Create(Guid taskId, Guid userId, int minutesSpent, string? description, DateOnly loggedDate)
        {
            var log = new TimeLog(taskId, userId, minutesSpent, description, loggedDate);
            log.OnCreate();
            return log;
        }
    }
}
