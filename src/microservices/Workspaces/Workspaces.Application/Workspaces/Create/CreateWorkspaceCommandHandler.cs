using DomainFramework.Results;
using Messaging.Abstractions;
using Workspaces.Application.Abstractions;
using Workspaces.Domain.Workspaces;

namespace Workspaces.Application.Workspaces.Create
{
    /// <summary>
    /// Handles <see cref="CreateWorkspaceCommand"/> — creates a new <see cref="Workspace"/>
    /// and registers the owner as the first member.
    /// </summary>
    internal sealed class CreateWorkspaceCommandHandler : ICommandHandler<CreateWorkspaceCommand, Result<Guid>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IWorkspaceRepository _workspaceRepository;

        public CreateWorkspaceCommandHandler(IUnitOfWork uow, IWorkspaceRepository workspaceRepository)
        {
            _uow = uow;
            _workspaceRepository = workspaceRepository;
        }

        /// <inheritdoc/>
        public async Task<Result<Guid>> Handle(CreateWorkspaceCommand command, CancellationToken cancellationToken)
        {
            var workspace = Workspace.Create(command.Name, command.OwnerId, command.Description);

            await _workspaceRepository.AddAsync(workspace, cancellationToken);
            await _uow.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Success(workspace.Id);
        }
    }
}
