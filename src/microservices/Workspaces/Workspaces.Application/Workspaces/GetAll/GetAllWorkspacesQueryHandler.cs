using DomainFramework;
using DomainFramework.Results;
using Messaging.Abstractions;
using Workspaces.Application.Workspaces.DTOs;
using Workspaces.Domain.Tasks;
using Workspaces.Domain.Workspaces;

namespace Workspaces.Application.Workspaces.GetAll
{
    /// <summary>
    /// Handles <see cref="GetAllWorkspacesQuery"/> — retrieves paginated workspaces.
    /// </summary>
    internal sealed class GetAllWorkspacesQueryHandler : IQueryHandler<GetAllWorkspacesQuery, Result<PaginationResult<WorkspaceResult>>>
    {
        private readonly IWorkspaceRepository _workspaceRepository;
        private readonly ITaskRepository? _taskRepository;

        public GetAllWorkspacesQueryHandler(
            IWorkspaceRepository workspaceRepository,
            ITaskRepository? taskRepository = null)
        {
            _workspaceRepository = workspaceRepository;
            _taskRepository = taskRepository;
        }

        /// <inheritdoc/>
        public async Task<Result<PaginationResult<WorkspaceResult>>> Handle(GetAllWorkspacesQuery query, CancellationToken cancellationToken)
        {
            var pagedWorkspaces = query.IsAdmin
                ? await _workspaceRepository.GetAllAsync(query.Name, query.Page, query.PageSize, cancellationToken)
                : await _workspaceRepository.GetByMemberAsync(query.RequestingUserId, query.Name, query.Page, query.PageSize, cancellationToken);

            var items = new List<WorkspaceResult>(pagedWorkspaces.Items.Count);
            foreach (var w in pagedWorkspaces.Items)
            {
                var taskCount = await GetCountByWorkspaceAsync(w.Id, cancellationToken);

                items.Add(new WorkspaceResult(
                    w.Id,
                    w.Name,
                    w.OwnerId,
                    w.Description,
                    w.Members.Count,
                    taskCount,
                    w.CreatedAt,
                    w.UpdatedAt));
            }

            var result = PaginationResult<WorkspaceResult>.Create(
                items,
                pagedWorkspaces.Page,
                pagedWorkspaces.PageSize,
                pagedWorkspaces.TotalCount);

            return Result<PaginationResult<WorkspaceResult>>.Success(result);
        }

        private Task<int> GetCountByWorkspaceAsync(Guid workspaceId, CancellationToken cancellationToken)
        {
            return _taskRepository is not null
                ? _taskRepository.GetCountByWorkspaceAsync(workspaceId, cancellationToken)
                : Task.FromResult(0);
        }
    }
}
