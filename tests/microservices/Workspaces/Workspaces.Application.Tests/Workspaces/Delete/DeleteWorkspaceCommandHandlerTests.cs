using DomainFramework;
using DomainFramework.Errors;
using FluentAssertions;
using Workspaces.Application.Abstractions;
using Workspaces.Application.Workspaces.Delete;
using Workspaces.Domain.Workspaces;
using Xunit;

namespace Workspaces.Application.Tests.Workspaces.Delete
{
    public class DeleteWorkspaceCommandHandlerTests
    {
        private readonly FakeUnitOfWork _uow = new();
        private readonly FakeWorkspaceRepository _repository = new();
        private readonly DeleteWorkspaceCommandHandler _handler;

        public DeleteWorkspaceCommandHandlerTests()
        {
            _handler = new DeleteWorkspaceCommandHandler(_uow, _repository);
        }

        [Fact]
        public async Task Handle_Should_ReturnNotFound_When_WorkspaceDoesNotExist()
        {
            // Arrange
            var command = new DeleteWorkspaceCommand(Guid.NewGuid(), Guid.NewGuid(), false);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().BeOfType<NotFoundError>();
        }

        [Fact]
        public async Task Handle_Should_ReturnForbidden_When_UserIsNotOwnerOrAdmin()
        {
            // Arrange
            var ownerId = Guid.NewGuid();
            var workspace = Workspace.Create("Dev", ownerId, null);
            _repository.Workspaces.Add(workspace);

            var strangerId = Guid.NewGuid();
            var command = new DeleteWorkspaceCommand(workspace.Id, strangerId, false);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().BeOfType<ForbiddenError>();
            _repository.Workspaces.Should().Contain(workspace);
        }

        [Fact]
        public async Task Handle_Should_DeleteWorkspace_When_UserIsOwner()
        {
            // Arrange
            var ownerId = Guid.NewGuid();
            var workspace = Workspace.Create("Dev", ownerId, null);
            _repository.Workspaces.Add(workspace);

            var command = new DeleteWorkspaceCommand(workspace.Id, ownerId, false);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            _repository.Workspaces.Should().NotContain(workspace);
        }

        [Fact]
        public async Task Handle_Should_DeleteWorkspace_When_UserIsAdmin()
        {
            // Arrange
            var ownerId = Guid.NewGuid();
            var workspace = Workspace.Create("Dev", ownerId, null);
            _repository.Workspaces.Add(workspace);

            var adminId = Guid.NewGuid();
            var command = new DeleteWorkspaceCommand(workspace.Id, adminId, true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            _repository.Workspaces.Should().NotContain(workspace);
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
