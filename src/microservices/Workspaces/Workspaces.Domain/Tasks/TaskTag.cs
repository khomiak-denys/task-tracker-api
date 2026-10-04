using Workspaces.Domain.Tags;

namespace Workspaces.Domain.Tasks
{
    public class TaskTag
    {
        public Guid TaskId { get; private set; }

        public Guid TagId { get; private set; }

        public TaskItem Task { get; private set; } = null!;

        public Tag Tag { get; private set; } = null!;

        private TaskTag() { }

        public TaskTag(Guid taskId, Guid tagId)
        {
            TaskId = taskId;
            TagId = tagId;
        }
    }
}
