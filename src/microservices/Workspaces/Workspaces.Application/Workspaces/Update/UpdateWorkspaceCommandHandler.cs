using DomainFramework.Errors;
using DomainFramework.Results;
using Messaging.Abstractions;
using Workspaces.Application.Abstractions;
using Workspaces.Domain.Workspaces;

namespace Workspaces.Application.Workspaces.Update
{
    /// <summary>
    /// Handles <see cref="UpdateWorkspaceCommand"/> — only the owner or an admin may update.
    /// </summary>
    internal sealed class UpdateWorkspaceCommandHandler : ICommandHandler<UpdateWorkspaceCommand, Result>
    {
        private readonly IUnitOfWork _uow;
        private readonly IWorkspaceRepository _workspaceRepository;

        public UpdateWorkspaceCommandHandler(IUnitOfWork uow, IWorkspaceRepository workspaceRepository)
        {
            _uow = uow;
            _workspaceRepository = workspaceRepository;
        }

        /// <inheritdoc/>
        public async Task<Result> Handle(UpdateWorkspaceCommand command, CancellationToken cancellationToken)
        {
            var workspace = await _workspaceRepository.GetByIdAsync(command.WorkspaceId, cancellationToken);
            if (workspace is null)
            {
                return Result.Failure(new NotFoundError($"Workspace '{command.WorkspaceId}' was not found."));
            }

            if (!command.IsAdmin && workspace.OwnerId != command.RequestingUserId)
            {
                return Result.Failure(new ForbiddenError("Only the workspace owner or an admin can update this workspace."));
            }

            workspace.Update(command.Name, command.Description);
            await _uow.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
