using Tasks.Application.Tasks.Update;
using Tasks.Domain.Tasks;

namespace Tasks.API.Tasks.Requests
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
