using DomainFramework;
using DomainFramework.Errors;
using FluentAssertions;
using Workspaces.Application.Abstractions;
using Workspaces.Application.Workspaces.Update;
using Workspaces.Domain.Workspaces;
using Xunit;

namespace Workspaces.Application.Tests.Workspaces.Update
{
    public class UpdateWorkspaceCommandHandlerTests
    {
        private readonly FakeUnitOfWork _uow = new();
        private readonly FakeWorkspaceRepository _repository = new();
        private readonly UpdateWorkspaceCommandHandler _handler;

        public UpdateWorkspaceCommandHandlerTests()
        {
            _handler = new UpdateWorkspaceCommandHandler(_uow, _repository);
        }

        [Fact]
        public async Task Handle_Should_ReturnNotFound_When_WorkspaceDoesNotExist()
        {
            // Arrange
            var command = new UpdateWorkspaceCommand(Guid.NewGuid(), Guid.NewGuid(), false, "New Name", null);

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
            var command = new UpdateWorkspaceCommand(workspace.Id, strangerId, false, "Hacked", null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().BeOfType<ForbiddenError>();
        }

        [Fact]
        public async Task Handle_Should_UpdateWorkspace_When_UserIsOwner()
        {
            // Arrange
            var ownerId = Guid.NewGuid();
            var workspace = Workspace.Create("Dev", ownerId, "Old Desc");
            _repository.Workspaces.Add(workspace);

            var command = new UpdateWorkspaceCommand(workspace.Id, ownerId, false, "New Dev", "New Desc");

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            workspace.Name.Should().Be("New Dev");
            workspace.Description.Should().Be("New Desc");
        }

        [Fact]
        public async Task Handle_Should_UpdateWorkspace_When_UserIsAdmin()
        {
            // Arrange
            var ownerId = Guid.NewGuid();
            var workspace = Workspace.Create("Dev", ownerId, "Old Desc");
            _repository.Workspaces.Add(workspace);

            var adminId = Guid.NewGuid();
            var command = new UpdateWorkspaceCommand(workspace.Id, adminId, true, "Admin Renamed", "Admin Desc");

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            workspace.Name.Should().Be("Admin Renamed");
            workspace.Description.Should().Be("Admin Desc");
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
