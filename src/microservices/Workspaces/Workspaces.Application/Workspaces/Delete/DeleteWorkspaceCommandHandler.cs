using DomainFramework.Errors;
using DomainFramework.Results;
using Messaging.Abstractions;
using Workspaces.Application.Abstractions;
using Workspaces.Domain.Workspaces;

namespace Workspaces.Application.Workspaces.Delete
{
    /// <summary>
    /// Handles <see cref="DeleteWorkspaceCommand"/> — only the owner or an admin may delete the workspace.
    /// </summary>
    internal sealed class DeleteWorkspaceCommandHandler : ICommandHandler<DeleteWorkspaceCommand, Result>
    {
        private readonly IUnitOfWork _uow;
        private readonly IWorkspaceRepository _workspaceRepository;

        public DeleteWorkspaceCommandHandler(IUnitOfWork uow, IWorkspaceRepository workspaceRepository)
        {
            _uow = uow;
            _workspaceRepository = workspaceRepository;
        }

        /// <inheritdoc/>
        public async Task<Result> Handle(DeleteWorkspaceCommand command, CancellationToken cancellationToken)
        {
            var workspace = await _workspaceRepository.GetByIdAsync(command.WorkspaceId, cancellationToken);
            if (workspace is null)
            {
                return Result.Failure(new NotFoundError($"Workspace '{command.WorkspaceId}' was not found."));
            }

            if (!command.IsAdmin && workspace.OwnerId != command.RequestingUserId)
            {
                return Result.Failure(new ForbiddenError("Only the workspace owner or an admin can delete this workspace."));
            }

            await _workspaceRepository.RemoveAsync(workspace, cancellationToken);
            await _uow.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
