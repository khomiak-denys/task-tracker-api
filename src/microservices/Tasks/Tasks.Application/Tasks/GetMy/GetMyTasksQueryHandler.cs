using DomainFramework;
using DomainFramework.Results;
using Messaging.Abstractions;
using Tasks.Application.Tasks.DTOs;
using Tasks.Domain.Tasks;

namespace Tasks.Application.Tasks.GetMy
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
            var pagedTasks = await _taskRepository.GetMyAsync(
                query.UserId,
                query.Type,
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
