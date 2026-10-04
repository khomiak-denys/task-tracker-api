using Workspaces.Application.Tasks.ChangeStatus;
using TaskStatus = Workspaces.Domain.Tasks.TaskStatus;

namespace Workspaces.API.Tasks.Requests
{
    public record ChangeTaskStatusRequest(TaskStatus Status)
    {
        public ChangeTaskStatusCommand ToCommand(Guid taskId, Guid requestedById)
        {
            return new ChangeTaskStatusCommand(taskId, requestedById, Status);
        }
    }
}
