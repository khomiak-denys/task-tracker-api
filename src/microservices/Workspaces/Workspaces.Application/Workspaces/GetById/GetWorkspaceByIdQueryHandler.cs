using DomainFramework.Errors;
using DomainFramework.Results;
using Messaging.Abstractions;
using Workspaces.Application.Workspaces.DTOs;
using Workspaces.Domain.Workspaces;

namespace Workspaces.Application.Workspaces.GetById
{
    /// <summary>
    /// Handles <see cref="GetWorkspaceByIdQuery"/> — only the owner or an admin may retrieve workspace details.
    /// </summary>
    internal sealed class GetWorkspaceByIdQueryHandler : IQueryHandler<GetWorkspaceByIdQuery, Result<WorkspaceDetailsResult>>
    {
        private readonly IWorkspaceRepository _workspaceRepository;

        public GetWorkspaceByIdQueryHandler(IWorkspaceRepository workspaceRepository)
        {
            _workspaceRepository = workspaceRepository;
        }

        /// <inheritdoc/>
        public async Task<Result<WorkspaceDetailsResult>> Handle(GetWorkspaceByIdQuery query, CancellationToken cancellationToken)
        {
            var workspace = await _workspaceRepository.GetByIdAsync(query.WorkspaceId, cancellationToken);
            if (workspace is null)
            {
                return Result<WorkspaceDetailsResult>.Failure(new NotFoundError($"Workspace '{query.WorkspaceId}' was not found."));
            }

            if (!query.IsAdmin && workspace.OwnerId != query.RequestingUserId)
            {
                return Result<WorkspaceDetailsResult>.Failure(new ForbiddenError("Only the workspace owner or an admin can view this workspace."));
            }

            var memberIds = workspace.Members
                .Select(m => m.UserId)
                .ToList();

            var result = new WorkspaceDetailsResult(
                workspace.Id,
                workspace.Name,
                workspace.OwnerId,
                workspace.Description,
                memberIds,
                workspace.CreatedAt,
                workspace.UpdatedAt);

            return Result<WorkspaceDetailsResult>.Success(result);
        }
    }
}
