using DomainFramework.Errors;
using DomainFramework.Results;
using Messaging.Abstractions;
using Tasks.Application.Tasks.DTOs;
using Tasks.Domain.Tasks;

namespace Tasks.Application.Tasks.GetById
{
    internal sealed class GetByIdQueryHandler : IQueryHandler<GetByIdQuery, Result<TaskDetailsResult>>
    {
        private readonly ITaskRepository _taskRepository;

        public GetByIdQueryHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<Result<TaskDetailsResult>> Handle(GetByIdQuery query, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetByIdAsync(query.TaskId, cancellationToken);
            if (task is null)
            {
                return Result<TaskDetailsResult>.Failure(new NotFoundError($"Task '{query.TaskId}' was not found."));
            }

            if (!query.IsAdmin && task.CreatedById != query.RequestingUserId && task.AssigneeId != query.RequestingUserId)
            {
                return Result<TaskDetailsResult>.Failure(new ForbiddenError("You are not allowed to view this task."));
            }

            var tags = task.TaskTags
                .Where(tt => tt.Tag is not null)
                .Select(tt => tt.Tag.Name)
                .ToList();

            var timeLogs = task.TimeLogs
                .Select(l => new TimeLogResult(
                    l.Id,
                    l.UserId,
                    l.MinutesSpent,
                    l.Description,
                    l.LoggedDate,
                    l.CreatedAt))
                .ToList();

            var result = new TaskDetailsResult(
                task.Id,
                task.Title,
                task.Description,
                task.Status,
                task.Priority,
                task.Deadline,
                task.AssigneeId,
                task.CreatedById,
                task.CreatedAt,
                task.UpdatedAt,
                tags,
                timeLogs);

            return Result<TaskDetailsResult>.Success(result);
        }
    }
}
