using DomainFramework.Errors;
using DomainFramework.Results;
using Messaging.Abstractions;
using Workspaces.Application.Abstractions;
using Workspaces.Domain.Tasks;

namespace Workspaces.Application.Tasks.Complete
{
    /// <summary>
    /// Handles <see cref="CompleteTaskCommand"/> — transitions the task to <c>Done</c> status.
    /// </summary>
    internal sealed class CompleteTaskCommandHandler : ICommandHandler<CompleteTaskCommand, Result>
    {
        private readonly IUnitOfWork _uow;
        private readonly ITaskRepository _taskRepository;

        public CompleteTaskCommandHandler(IUnitOfWork uow, ITaskRepository taskRepository)
        {
            _uow = uow;
            _taskRepository = taskRepository;
        }

        /// <inheritdoc/>
        public async Task<Result> Handle(CompleteTaskCommand command, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetByIdAsync(command.TaskId, cancellationToken);
            if (task is null)
            {
                return Result.Failure(new NotFoundError($"Task '{command.TaskId}' was not found."));
            }

            var result = task.Complete();
            if (result.IsFailure)
            {
                return result;
            }

            await _uow.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
