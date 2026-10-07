using DomainFramework;
using FluentAssertions;
using Workspaces.Application.Tasks.GetAll;
using Workspaces.Domain.Tasks;
using TaskStatus = Workspaces.Domain.Tasks.TaskStatus;
using Xunit;

namespace Workspaces.Application.Tests.Tasks.GetAll
{
    public class GetAllTasksQueryHandlerTests
    {
        private readonly FakeTaskRepository _repository = new();
        private readonly GetAllTasksQueryHandler _handler;

        public GetAllTasksQueryHandlerTests()
        {
            _handler = new GetAllTasksQueryHandler(_repository);
        }

        [Fact]
        public async Task Handle_Should_ReturnAllTasks_When_QueryHasDefaults()
        {
            var workspaceId = Guid.NewGuid();
            var task = TaskItem.Create(workspaceId, "Task 1", "Desc", Priority.Medium, null, null, Guid.NewGuid());
            _repository.TasksToReturn.Add(task);

            var query = new GetAllTasksQuery();
            var result = await _handler.Handle(query, CancellationToken.None);

            result.IsSuccess.Should().BeTrue();
            result.Value.Items.Should().HaveCount(1);
            result.Value.Items[0].Title.Should().Be("Task 1");
            _repository.LastWorkspaceId.Should().BeNull();
            _repository.LastSearch.Should().BeNull();
            _repository.LastStatus.Should().BeNull();
            _repository.LastPriority.Should().BeNull();
        }

        [Fact]
        public async Task Handle_Should_PassWorkspaceId_When_Provided()
        {
            var workspaceId = Guid.NewGuid();
            var query = new GetAllTasksQuery(WorkspaceId: workspaceId);

            var result = await _handler.Handle(query, CancellationToken.None);

            result.IsSuccess.Should().BeTrue();
            _repository.LastWorkspaceId.Should().Be(workspaceId);
        }

        [Fact]
        public async Task Handle_Should_ParseStatusAndPriority_When_Provided()
        {
            var query = new GetAllTasksQuery(
                Search: "fix bug",
                Status: "InProgress",
                Priority: "High",
                Tag: "backend",
                Page: 2,
                PageSize: 20);

            var result = await _handler.Handle(query, CancellationToken.None);

            result.IsSuccess.Should().BeTrue();
            _repository.LastSearch.Should().Be("fix bug");
            _repository.LastStatus.Should().Be(TaskStatus.InProgress);
            _repository.LastPriority.Should().Be(Priority.High);
            _repository.LastTag.Should().Be("backend");
            _repository.LastPage.Should().Be(2);
            _repository.LastPageSize.Should().Be(20);
        }

        [Fact]
        public async Task Handle_Should_TreatAllAsNull_For_StatusAndPriority()
        {
            var query = new GetAllTasksQuery(
                Status: "all",
                Priority: "ALL");

            var result = await _handler.Handle(query, CancellationToken.None);

            result.IsSuccess.Should().BeTrue();
            _repository.LastStatus.Should().BeNull();
            _repository.LastPriority.Should().BeNull();
        }

        [Fact]
        public async Task Handle_Should_SetEffectiveAssigneeId_When_TypeIsAssigned()
        {
            var userId = Guid.NewGuid();
            var query = new GetAllTasksQuery(
                Type: "assigned",
                RequestingUserId: userId);

            var result = await _handler.Handle(query, CancellationToken.None);

            result.IsSuccess.Should().BeTrue();
            _repository.LastAssigneeId.Should().Be(userId);
            _repository.LastCreatedById.Should().BeNull();
        }

        [Fact]
        public async Task Handle_Should_SetEffectiveCreatedById_When_TypeIsCreated()
        {
            var userId = Guid.NewGuid();
            var query = new GetAllTasksQuery(
                Type: "created",
                RequestingUserId: userId);

            var result = await _handler.Handle(query, CancellationToken.None);

            result.IsSuccess.Should().BeTrue();
            _repository.LastCreatedById.Should().Be(userId);
            _repository.LastAssigneeId.Should().BeNull();
        }

        private sealed class FakeTaskRepository : ITaskRepository
        {
            public List<TaskItem> TasksToReturn { get; } = new();

            public Guid? LastWorkspaceId { get; private set; }
            public string? LastSearch { get; private set; }
            public TaskStatus? LastStatus { get; private set; }
            public Priority? LastPriority { get; private set; }
            public Guid? LastAssigneeId { get; private set; }
            public Guid? LastCreatedById { get; private set; }
            public string? LastTag { get; private set; }
            public int LastPage { get; private set; }
            public int LastPageSize { get; private set; }

            public Task<PaginationResult<TaskItem>> GetAllAsync(
                Guid? workspaceId,
                string? search,
                TaskStatus? status,
                Priority? priority,
                Guid? assigneeId,
                Guid? createdById,
                string? tag,
                int page,
                int pageSize,
                CancellationToken cancellationToken)
            {
                LastWorkspaceId = workspaceId;
                LastSearch = search;
                LastStatus = status;
                LastPriority = priority;
                LastAssigneeId = assigneeId;
                LastCreatedById = createdById;
                LastTag = tag;
                LastPage = page;
                LastPageSize = pageSize;

                var result = PaginationResult<TaskItem>.Create(TasksToReturn, page, pageSize, TasksToReturn.Count);
                return Task.FromResult(result);
            }

            public Task<PaginationResult<TaskItem>> GetMyAsync(
                Guid userId,
                string? type,
                string? search,
                TaskStatus? status,
                Priority? priority,
                string? tag,
                int page,
                int pageSize,
                CancellationToken cancellationToken) =>
                Task.FromResult(PaginationResult<TaskItem>.Create(Array.Empty<TaskItem>(), page, pageSize, 0));

            public Task<TaskItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken) => Task.FromResult<TaskItem?>(null);
            public Task AddAsync(TaskItem task, CancellationToken cancellationToken) => Task.CompletedTask;
            public Task RemoveAsync(TaskItem task, CancellationToken cancellationToken) => Task.CompletedTask;
        }
    }
}
