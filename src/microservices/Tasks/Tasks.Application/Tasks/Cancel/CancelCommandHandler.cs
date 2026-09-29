using DomainFramework.Errors;
using DomainFramework.Results;
using Messaging.Abstractions;
using Tasks.Application.Abstractions;
using Tasks.Domain.Tasks;

namespace Tasks.Application.Tasks.Cancel
{
    /// <summary>
    /// Handles <see cref="CancelCommand"/> — transitions the task to <c>Cancelled</c> status.
    /// </summary>
    internal sealed class CancelCommandHandler : ICommandHandler<CancelCommand, Result>
    {
        private readonly IUnitOfWork _uow;
        private readonly ITaskRepository _taskRepository;

        public CancelCommandHandler(IUnitOfWork uow, ITaskRepository taskRepository)
        {
            _uow = uow;
            _taskRepository = taskRepository;
        }

        /// <inheritdoc/>
        public async Task<Result> Handle(CancelCommand command, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetByIdAsync(command.TaskId, cancellationToken);
            if (task is null)
            {
                return Result.Failure(new NotFoundError($"Task '{command.TaskId}' was not found."));
            }

            var result = task.Cancel();
            if (result.IsFailure)
            {
                return result;
            }

            await _uow.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
