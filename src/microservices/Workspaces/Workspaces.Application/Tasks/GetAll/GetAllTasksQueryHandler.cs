using DomainFramework;
using DomainFramework.Results;
using Messaging.Abstractions;
using Workspaces.Application.Tasks.DTOs;
using Workspaces.Domain.Tasks;

namespace Workspaces.Application.Tasks.GetAll
{
    /// <summary>
    /// Handles <see cref="GetAllTasksQuery"/> — retrieves paged tasks.
    /// </summary>
    internal sealed class GetAllTasksQueryHandler : IQueryHandler<GetAllTasksQuery, Result<PaginationResult<TaskResult>>>
    {
        private readonly ITaskRepository _taskRepository;

        public GetAllTasksQueryHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<Result<PaginationResult<TaskResult>>> Handle(GetAllTasksQuery query, CancellationToken cancellationToken)
        {
            var pagedTasks = await _taskRepository.GetAllAsync(
                query.Page,
                query.PageSize,
                cancellationToken);

            var items = pagedTasks.Items
                .Select(task => new TaskResult(
                    task.Id,
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
