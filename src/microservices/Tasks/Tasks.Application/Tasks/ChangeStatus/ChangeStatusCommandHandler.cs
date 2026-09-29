using DomainFramework.Errors;
using DomainFramework.Results;
using Messaging.Abstractions;
using Tasks.Application.Abstractions;
using Tasks.Domain.Tasks;

namespace Tasks.Application.Tasks.ChangeStatus
{
    /// <summary>
    /// Handles <see cref="ChangeStatusCommand"/> — delegates transition logic to the domain entity.
    /// </summary>
    internal sealed class ChangeStatusCommandHandler : ICommandHandler<ChangeStatusCommand, Result>
    {
        private readonly IUnitOfWork _uow;
        private readonly ITaskRepository _taskRepository;

        public ChangeStatusCommandHandler(IUnitOfWork uow, ITaskRepository taskRepository)
        {
            _uow = uow;
            _taskRepository = taskRepository;
        }

        /// <inheritdoc/>
        public async Task<Result> Handle(ChangeStatusCommand command, CancellationToken cancellationToken)
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
