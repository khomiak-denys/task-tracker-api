using DomainFramework;
using DomainFramework.Errors;
using FluentAssertions;
using Workspaces.Application.Workspaces.GetById;
using Workspaces.Domain.Workspaces;
using Xunit;

namespace Workspaces.Application.Tests.Workspaces.GetById
{
    public class GetWorkspaceByIdQueryHandlerTests
    {
        private readonly FakeWorkspaceRepository _repository = new();
        private readonly GetWorkspaceByIdQueryHandler _handler;

        public GetWorkspaceByIdQueryHandlerTests()
        {
            _handler = new GetWorkspaceByIdQueryHandler(_repository);
        }

        [Fact]
        public async Task Handle_Should_ReturnNotFound_When_WorkspaceDoesNotExist()
        {
            // Arrange
            var query = new GetWorkspaceByIdQuery(Guid.NewGuid(), Guid.NewGuid(), false);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().BeOfType<NotFoundError>();
        }

        [Fact]
        public async Task Handle_Should_ReturnForbidden_When_UserIsNotOwnerOrAdmin()
        {
            // Arrange
            var ownerId = Guid.NewGuid();
            var workspace = Workspace.Create("Secret", ownerId, "Top Secret");
            _repository.Workspaces.Add(workspace);

            var strangerId = Guid.NewGuid();
            var query = new GetWorkspaceByIdQuery(workspace.Id, strangerId, false);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().BeOfType<ForbiddenError>();
        }

        [Fact]
        public async Task Handle_Should_ReturnWorkspaceDetails_When_UserIsOwner()
        {
            // Arrange
            var ownerId = Guid.NewGuid();
            var workspace = Workspace.Create("Dev Workspace", ownerId, "Dev Description");
            var memberId = Guid.NewGuid();
            workspace.AddMember(memberId);
            _repository.Workspaces.Add(workspace);

            var query = new GetWorkspaceByIdQuery(workspace.Id, ownerId, false);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Id.Should().Be(workspace.Id);
            result.Value.Name.Should().Be("Dev Workspace");
            result.Value.OwnerId.Should().Be(ownerId);
            result.Value.Description.Should().Be("Dev Description");
            result.Value.MemberIds.Should().HaveCount(2);
            result.Value.MemberIds.Should().Contain(ownerId);
            result.Value.MemberIds.Should().Contain(memberId);
        }

        [Fact]
        public async Task Handle_Should_ReturnWorkspaceDetails_When_UserIsAdmin()
        {
            // Arrange
            var ownerId = Guid.NewGuid();
            var workspace = Workspace.Create("Dev Workspace", ownerId, "Dev Description");
            _repository.Workspaces.Add(workspace);

            var adminId = Guid.NewGuid();
            var query = new GetWorkspaceByIdQuery(workspace.Id, adminId, true);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Id.Should().Be(workspace.Id);
            result.Value.Name.Should().Be("Dev Workspace");
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
