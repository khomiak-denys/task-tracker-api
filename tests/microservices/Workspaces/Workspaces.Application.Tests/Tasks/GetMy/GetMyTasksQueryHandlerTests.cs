using DomainFramework;
using FluentAssertions;
using Workspaces.Application.Tasks.GetMy;
using Workspaces.Domain.Tasks;
using TaskStatus = Workspaces.Domain.Tasks.TaskStatus;
using Xunit;

namespace Workspaces.Application.Tests.Tasks.GetMy
{
    public class GetMyTasksQueryHandlerTests
    {
        private readonly FakeTaskRepository _repository = new();
        private readonly GetMyTasksQueryHandler _handler;

        public GetMyTasksQueryHandlerTests()
        {
            _handler = new GetMyTasksQueryHandler(_repository);
        }

        [Fact]
        public async Task Handle_Should_ReturnTasks_When_QueryHasDefaults()
        {
            var userId = Guid.NewGuid();
            var workspaceId = Guid.NewGuid();
            var task = TaskItem.Create(workspaceId, "My Task", "Desc", Priority.Medium, null, userId, userId);
            _repository.TasksToReturn.Add(task);

            var query = new GetMyTasksQuery(userId);
            var result = await _handler.Handle(query, CancellationToken.None);

            result.IsSuccess.Should().BeTrue();
            result.Value.Items.Should().HaveCount(1);
            result.Value.Items[0].Title.Should().Be("My Task");
            _repository.LastUserId.Should().Be(userId);
            _repository.LastType.Should().BeNull();
            _repository.LastSearch.Should().BeNull();
            _repository.LastStatus.Should().BeNull();
            _repository.LastPriority.Should().BeNull();
            _repository.LastTag.Should().BeNull();
        }

        [Fact]
        public async Task Handle_Should_PassAllFiltersToRepository_When_Specified()
        {
            var userId = Guid.NewGuid();
            var query = new GetMyTasksQuery(
                UserId: userId,
                Type: "assigned",
                Search: "deploy",
                Status: "InReview",
                Priority: "Critical",
                Tag: "ops",
                Page: 3,
                PageSize: 15);

            var result = await _handler.Handle(query, CancellationToken.None);

            result.IsSuccess.Should().BeTrue();
            _repository.LastUserId.Should().Be(userId);
            _repository.LastType.Should().Be("assigned");
            _repository.LastSearch.Should().Be("deploy");
            _repository.LastStatus.Should().Be(TaskStatus.InReview);
            _repository.LastPriority.Should().Be(Priority.Critical);
            _repository.LastTag.Should().Be("ops");
            _repository.LastPage.Should().Be(3);
            _repository.LastPageSize.Should().Be(15);
        }

        [Fact]
        public async Task Handle_Should_ParseAllAsNull_For_StatusAndPriority()
        {
            var userId = Guid.NewGuid();
            var query = new GetMyTasksQuery(
                UserId: userId,
                Status: "ALL",
                Priority: "all");

            var result = await _handler.Handle(query, CancellationToken.None);

            result.IsSuccess.Should().BeTrue();
            _repository.LastStatus.Should().BeNull();
            _repository.LastPriority.Should().BeNull();
        }

        private sealed class FakeTaskRepository : ITaskRepository
        {
            public List<TaskItem> TasksToReturn { get; } = new();

            public Guid LastUserId { get; private set; }
            public string? LastType { get; private set; }
            public string? LastSearch { get; private set; }
            public TaskStatus? LastStatus { get; private set; }
            public Priority? LastPriority { get; private set; }
            public string? LastTag { get; private set; }
            public int LastPage { get; private set; }
            public int LastPageSize { get; private set; }

            public Task<PaginationResult<TaskItem>> GetMyAsync(
                Guid userId,
                string? type,
                string? search,
                TaskStatus? status,
                Priority? priority,
                string? tag,
                int page,
                int pageSize,
                CancellationToken cancellationToken)
            {
                LastUserId = userId;
                LastType = type;
                LastSearch = search;
                LastStatus = status;
                LastPriority = priority;
                LastTag = tag;
                LastPage = page;
                LastPageSize = pageSize;

                var result = PaginationResult<TaskItem>.Create(TasksToReturn, page, pageSize, TasksToReturn.Count);
                return Task.FromResult(result);
            }

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
                CancellationToken cancellationToken) =>
                Task.FromResult(PaginationResult<TaskItem>.Create(Array.Empty<TaskItem>(), page, pageSize, 0));

            public Task<TaskItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken) => Task.FromResult<TaskItem?>(null);
            public Task AddAsync(TaskItem task, CancellationToken cancellationToken) => Task.CompletedTask;
            public Task RemoveAsync(TaskItem task, CancellationToken cancellationToken) => Task.CompletedTask;
        }
    }
}
