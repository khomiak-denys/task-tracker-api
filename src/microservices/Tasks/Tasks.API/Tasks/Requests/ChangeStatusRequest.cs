using Tasks.Application.Tasks.ChangeStatus;
using TaskStatus = Tasks.Domain.Tasks.TaskStatus;

namespace Tasks.API.Tasks.Requests
{
    public record ChangeStatusRequest(TaskStatus Status)
    {
        public ChangeStatusCommand ToCommand(Guid taskId, Guid requestedById)
        {
            return new ChangeStatusCommand(taskId, requestedById, Status);
        }
    }
}
