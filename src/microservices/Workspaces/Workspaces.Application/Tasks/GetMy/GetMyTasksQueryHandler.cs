using DomainFramework;
using DomainFramework.Results;
using Messaging.Abstractions;
using Workspaces.Application.Tasks.DTOs;
using Workspaces.Domain.Tasks;
using TaskStatus = Workspaces.Domain.Tasks.TaskStatus;

namespace Workspaces.Application.Tasks.GetMy
{
    /// <summary>
    /// Handles <see cref="GetMyTasksQuery"/> — retrieves paged tasks for the user.
    /// </summary>
    internal sealed class GetMyTasksQueryHandler : IQueryHandler<GetMyTasksQuery, Result<PaginationResult<TaskResult>>>
    {
        private readonly ITaskRepository _taskRepository;

        public GetMyTasksQueryHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<Result<PaginationResult<TaskResult>>> Handle(GetMyTasksQuery query, CancellationToken cancellationToken)
        {
            TaskStatus? status = !string.IsNullOrWhiteSpace(query.Status) && !query.Status.Equals("all", StringComparison.OrdinalIgnoreCase) && Enum.TryParse<TaskStatus>(query.Status, true, out var parsedStatus)
                ? parsedStatus
                : null;

            Priority? priority = !string.IsNullOrWhiteSpace(query.Priority) && !query.Priority.Equals("all", StringComparison.OrdinalIgnoreCase) && Enum.TryParse<Priority>(query.Priority, true, out var parsedPriority)
                ? parsedPriority
                : null;

            var pagedTasks = await _taskRepository.GetMyAsync(
                query.UserId,
                query.Type,
                query.Search,
                status,
                priority,
                query.Tag,
                query.Page,
                query.PageSize,
                cancellationToken);

            var items = pagedTasks.Items
                .Select(task => new TaskResult(
                    task.Id,
                    task.WorkspaceId,
                    task.Title,
                    task.Description,
                    task.Status,
                    task.Priority,
                    task.Deadline,
                    task.AssigneeId,
                    task.TaskTags
                        .Where(tt => tt.Tag is not null)
                        .Select(tt => tt.Tag.Name)
                        .ToList()))
                .ToList();

            var result = PaginationResult<TaskResult>.Create(
                items,
                pagedTasks.Page,
                pagedTasks.PageSize,
                pagedTasks.TotalCount);

            return Result<PaginationResult<TaskResult>>.Success(result);
        }
    }
}
