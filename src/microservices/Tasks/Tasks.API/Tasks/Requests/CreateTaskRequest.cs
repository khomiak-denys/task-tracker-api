using Tasks.Application.Tasks.Create;
using Tasks.Domain.Tasks;

namespace Tasks.API.Tasks.Requests
{
    public record CreateTaskRequest(
        string Title,
        string? Description,
        Priority Priority,
        DateTime? Deadline,
        Guid? AssigneeId,
        IEnumerable<string>? Tags)
    {
        public CreateTaskCommand ToCommand(Guid createdById)
        {
            return new CreateTaskCommand(
                createdById,
                Title,
                Description,
                Priority,
                Deadline,
                AssigneeId,
                Tags ?? Enumerable.Empty<string>());
        }
    }
}
