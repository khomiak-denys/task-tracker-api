using DomainFramework.Errors;
using DomainFramework.Results;
using Messaging.Abstractions;
using Tasks.Application.Abstractions;
using Tasks.Application.Tasks.DTOs;
using Tasks.Domain.Tasks;

namespace Tasks.Application.Tasks.GetById
{
    /// <summary>
    /// Handles <see cref="GetTaskByIdQuery"/> — retrieves task details including enriched user information.
    /// </summary>
    internal sealed class GetTaskByIdQueryHandler : IQueryHandler<GetTaskByIdQuery, Result<TaskDetailsResult>>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IUsersApiClient _usersApiClient;

        public GetTaskByIdQueryHandler(ITaskRepository taskRepository, IUsersApiClient usersApiClient)
        {
            _taskRepository = taskRepository;
            _usersApiClient = usersApiClient;
        }

        public async Task<Result<TaskDetailsResult>> Handle(GetTaskByIdQuery query, CancellationToken cancellationToken)
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

            var createdUserTask = _usersApiClient.GetByIdAsync(task.CreatedById, cancellationToken);
            var assignedUserTask = task.AssigneeId.HasValue
                ? _usersApiClient.GetByIdAsync(task.AssigneeId.Value, cancellationToken)
                : Task.FromResult<UserResult?>(null);

            await Task.WhenAll(createdUserTask, assignedUserTask);

            var createdUser = await createdUserTask
                ?? new UserResult(task.CreatedById, string.Empty, "Unknown", null);
            var assignedUser = await assignedUserTask;

            var result = new TaskDetailsResult(
                task.Id,
                task.Title,
                task.Description,
                task.Status,
                task.Priority,
                task.Deadline,
                assignedUser,
                createdUser,
                task.CreatedAt,
                task.UpdatedAt,
                tags,
                timeLogs);

            return Result<TaskDetailsResult>.Success(result);
        }
    }
}
