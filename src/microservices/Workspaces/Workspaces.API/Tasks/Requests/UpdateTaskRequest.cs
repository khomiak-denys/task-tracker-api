using Workspaces.Application.Tasks.Update;
using Workspaces.Domain.Tasks;

namespace Workspaces.API.Tasks.Requests
{
    public record UpdateTaskRequest(
        string Title,
        string? Description,
        Priority Priority,
        DateTime? Deadline,
        IEnumerable<string>? Tags)
    {
        public UpdateTaskCommand ToCommand(Guid taskId, Guid requestedById)
        {
            return new UpdateTaskCommand(
                taskId,
                requestedById,
                Title,
                Description,
                Priority,
                Deadline,
                Tags ?? Enumerable.Empty<string>());
        }
    }
}
