using DomainFramework.Errors;
using DomainFramework.Results;
using Messaging.Abstractions;
using Tasks.Application.Abstractions;
using Tasks.Domain.Tasks;

namespace Tasks.Application.Tasks.Assign
{
    internal sealed class AssignCommandHandler : ICommandHandler<AssignCommand, Result>
    {
        private readonly IUnitOfWork _uow;
        private readonly ITaskRepository _taskRepository;
        private readonly IUsersApiClient _usersApiClient;

        public AssignCommandHandler(
            IUnitOfWork uow,
            ITaskRepository taskRepository,
            IUsersApiClient usersApiClient)
        {
            _uow = uow;
            _taskRepository = taskRepository;
            _usersApiClient = usersApiClient;
        }

        public async Task<Result> Handle(AssignCommand command, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetByIdAsync(command.TaskId, cancellationToken);
            if (task is null)
            {
                return Result.Failure(new NotFoundError($"Task '{command.TaskId}' was not found."));
            }

            var user = await _usersApiClient.GetByIdAsync(command.AssigneeId, cancellationToken);
            if (user is null)
            {
                return Result.Failure(new NotFoundError($"User '{command.AssigneeId}' was not found."));
            }

            task.Assign(command.AssigneeId);

            await _uow.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
