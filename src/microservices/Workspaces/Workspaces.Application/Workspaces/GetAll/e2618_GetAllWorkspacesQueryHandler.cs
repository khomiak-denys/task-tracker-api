using DomainFramework;
using DomainFramework.Results;
using Messaging.Abstractions;
using Workspaces.Application.Workspaces.DTOs;
using Workspaces.Domain.Workspaces;

namespace Workspaces.Application.Workspaces.GetAll
{
    /// <summary>
    /// Handles <see cref="GetAllWorkspacesQuery"/> — retrieves paginated workspaces.
    /// </summary>
    internal sealed class GetAllWorkspacesQueryHandler : IQueryHandler<GetAllWorkspacesQuery, Result<PaginationResult<WorkspaceResult>>>
    {
        private readonly IWorkspaceRepository _workspaceRepository;

        public GetAllWorkspacesQueryHandler(IWorkspaceRepository workspaceRepository)
        {
            _workspaceRepository = workspaceRepository;
        }

        /// <inheritdoc/>
        public async Task<Result<PaginationResult<WorkspaceResult>>> Handle(GetAllWorkspacesQuery query, CancellationToken cancellationToken)
        {
            var pagedWorkspaces = query.IsAdmin
                ? await _workspaceRepository.GetAllAsync(query.Name, query.Page, query.PageSize, cancellationToken)
                : await _workspaceRepository.GetByMemberAsync(query.RequestingUserId, query.Name, query.Page, query.PageSize, cancellationToken);

            var items = pagedWorkspaces.Items
                .Select(w => new WorkspaceResult(
                    w.Id,
                    w.Name,
                    w.OwnerId,
                    w.Description,
                    w.Members.Count,
                    w.CreatedAt,
                    w.UpdatedAt))
                .ToList();

            var result = PaginationResult<WorkspaceResult>.Create(
                items,
                pagedWorkspaces.Page,
                pagedWorkspaces.PageSize,
                pagedWorkspaces.TotalCount);

            return Result<PaginationResult<WorkspaceResult>>.Success(result);
        }
    }
}
