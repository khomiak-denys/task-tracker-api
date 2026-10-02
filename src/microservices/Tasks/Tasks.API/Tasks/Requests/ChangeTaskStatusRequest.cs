using Tasks.Application.Tasks.ChangeStatus;
using TaskStatus = Tasks.Domain.Tasks.TaskStatus;

namespace Tasks.API.Tasks.Requests
{
    public record ChangeTaskStatusRequest(TaskStatus Status)
    {
        public ChangeTaskStatusCommand ToCommand(Guid taskId, Guid requestedById)
        {
            return new ChangeTaskStatusCommand(taskId, requestedById, Status);
        }
    }
}
