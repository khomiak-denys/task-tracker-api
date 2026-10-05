using DomainFramework.Errors;
using DomainFramework.Results;
using Messaging.Abstractions;
using Workspaces.Application.Abstractions;
using Workspaces.Domain.Tasks;

namespace Workspaces.Application.Tasks.ChangeStatus
{
    /// <summary>
    /// Handles <see cref="ChangeTaskStatusCommand"/> — delegates transition logic to the domain entity.
    /// </summary>
    internal sealed class ChangeTaskStatusCommandHandler : ICommandHandler<ChangeTaskStatusCommand, Result>
    {
        private readonly IUnitOfWork _uow;
        private readonly ITaskRepository _taskRepository;

        public ChangeTaskStatusCommandHandler(IUnitOfWork uow, ITaskRepository taskRepository)
        {
            _uow = uow;
            _taskRepository = taskRepository;
        }

        /// <inheritdoc/>
        public async Task<Result> Handle(ChangeTaskStatusCommand command, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetByIdAsync(command.TaskId, cancellationToken);
            if (task is null)
            {
                return Result.Failure(new NotFoundError($"Task '{command.TaskId}' was not found."));
            }

            var result = task.ChangeStatus(command.NewStatus);
            if (result.IsFailure)
            {
                return result;
            }

            await _uow.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
