using DomainFramework;
using DomainFramework.Errors;
using DomainFramework.Results;
using TaskStatus = Tasks.Domain.Tasks.TaskStatus;

namespace Tasks.Domain.Tasks
{
    public class TaskItem : EntityBase
    {
        public string Title { get; private set; } = null!;

        public string? Description { get; private set; }

        public TaskStatus Status { get; private set; }

        public Priority Priority { get; private set; }

        public DateTime? Deadline { get; private set; }

        public Guid? AssigneeId { get; private set; }

        public Guid CreatedById { get; private set; }

        public ICollection<TimeLog> TimeLogs { get; private set; } = new List<TimeLog>();

        public ICollection<TaskTag> TaskTags { get; private set; } = new List<TaskTag>();

        private TaskItem() { }

        private TaskItem(
            string title,
            string? description,
            Priority priority,
            DateTime? deadline,
            Guid? assigneeId,
            Guid createdById)
        {
            Title = title;
            Description = description;
            Priority = priority;
            Deadline = deadline;
            AssigneeId = assigneeId;
            CreatedById = createdById;
            Status = TaskStatus.Todo;
        }

        public static TaskItem Create(
            string title,
            string? description,
            Priority priority,
            DateTime? deadline,
            Guid? assigneeId,
            Guid createdById)
        {
            var task = new TaskItem(title, description, priority, deadline, assigneeId, createdById);
            task.OnCreate();
            return task;
        }

        public void Update(string title, string? description, Priority priority, DateTime? deadline)
        {
            Title = title;
            Description = description;
            Priority = priority;
            Deadline = deadline;
            OnModify();
        }

        public void Assign(Guid assigneeId)
        {
            AssigneeId = assigneeId;
            OnModify();
        }

        public Result ChangeStatus(TaskStatus newStatus)
        {
            var allowed = Status switch
            {
                TaskStatus.Todo => newStatus == TaskStatus.InProgress,
                TaskStatus.InProgress => newStatus == TaskStatus.InReview,
                TaskStatus.InReview => false,
                _ => false
            };

            if (!allowed)
            {
                return Result.Failure(new InvalidArgumentError($"Cannot transition task from '{Status}' to '{newStatus}'."));
            }

            Status = newStatus;
            OnModify();
            return Result.Success();
        }

        public void LogTime(Guid userId, int minutesSpent, string? description, DateOnly loggedDate)
        {
            var log = TimeLog.Create(Id, userId, minutesSpent, description, loggedDate);
            TimeLogs.Add(log);
            OnModify();
        }

        public Result Complete()
        {
            if (Status != TaskStatus.InReview)
            {
                return Result.Failure(new InvalidArgumentError($"Task can only be completed from 'InReview' status. Current status: '{Status}'."));
            }

            Status = TaskStatus.Done;
            OnModify();
            return Result.Success();
        }

        public Result Cancel()
        {
            if (Status == TaskStatus.Done)
            {
                return Result.Failure(new InvalidArgumentError("A completed task cannot be cancelled."));
            }

            if (Status == TaskStatus.Cancelled)
            {
                return Result.Failure(new InvalidArgumentError("Task is already cancelled."));
            }

            Status = TaskStatus.Cancelled;
            OnModify();
            return Result.Success();
        }
    }
}
