using FluentAssertions;
using Workspaces.Application.Abstractions;
using Workspaces.Application.Workspaces.Create;
using Workspaces.Domain.Workspaces;
using DomainFramework;
using Xunit;

namespace Workspaces.Application.Tests.Workspaces.Create
{
    public class CreateWorkspaceCommandHandlerTests
    {
        private readonly FakeUnitOfWork _uow = new();
        private readonly FakeWorkspaceRepository _repository = new();
        private readonly CreateWorkspaceCommandHandler _handler;

        public CreateWorkspaceCommandHandlerTests()
        {
            _handler = new CreateWorkspaceCommandHandler(_uow, _repository);
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccessWithWorkspaceId_When_ValidCommand()
        {
            // Arrange
            var ownerId = Guid.NewGuid();
            var command = new CreateWorkspaceCommand("Engineering", "Tech team", ownerId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeEmpty();
            _repository.Workspaces.Should().HaveCount(1);
            _repository.Workspaces.First().Name.Should().Be("Engineering");
            _repository.Workspaces.First().OwnerId.Should().Be(ownerId);
            _repository.Workspaces.First().Members.Should().Contain(m => m.UserId == ownerId);
        }

        private sealed class FakeUnitOfWork : IUnitOfWork
        {
            public Task<int> SaveChangesAsync(CancellationToken cancellationToken) => Task.FromResult(1);
        }

        private sealed class FakeWorkspaceRepository : IWorkspaceRepository
        {
            public List<Workspace> Workspaces { get; } = new();

            public Task<Workspace?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
                Task.FromResult(Workspaces.FirstOrDefault(w => w.Id == id));

            public Task<PaginationResult<Workspace>> GetByMemberAsync(Guid userId, string? name, int page, int pageSize, CancellationToken cancellationToken) =>
                Task.FromResult(PaginationResult<Workspace>.Create(Array.Empty<Workspace>(), page, pageSize, 0));

            public Task<PaginationResult<Workspace>> GetAllAsync(string? name, int page, int pageSize, CancellationToken cancellationToken) =>
                Task.FromResult(PaginationResult<Workspace>.Create(Array.Empty<Workspace>(), page, pageSize, 0));

            public Task AddAsync(Workspace workspace, CancellationToken cancellationToken)
            {
                Workspaces.Add(workspace);
                return Task.CompletedTask;
            }

            public Task RemoveAsync(Workspace workspace, CancellationToken cancellationToken)
            {
                Workspaces.Remove(workspace);
                return Task.CompletedTask;
            }
        }
    }
}
