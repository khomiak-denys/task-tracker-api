using FluentAssertions;
using Workspaces.Application.Tasks.GetAll;
using Xunit;

namespace Workspaces.Application.Tests.Tasks.GetAll
{
    public class GetAllTasksQueryValidatorTests
    {
        private readonly GetAllTasksQueryValidator _validator = new();

        [Fact]
        public void Validate_Should_Pass_When_QueryHasDefaultValues()
        {
            var query = new GetAllTasksQuery();
            var result = _validator.Validate(query);
            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Validate_Should_Fail_When_PageIsLessThanOne(int page)
        {
            var query = new GetAllTasksQuery(Page: page);
            var result = _validator.Validate(query);
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == nameof(GetAllTasksQuery.Page));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(101)]
        public void Validate_Should_Fail_When_PageSizeIsOutOfRange(int pageSize)
        {
            var query = new GetAllTasksQuery(PageSize: pageSize);
            var result = _validator.Validate(query);
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == nameof(GetAllTasksQuery.PageSize));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("all")]
        [InlineData("ALL")]
        [InlineData("created")]
        [InlineData("Created")]
        [InlineData("assigned")]
        [InlineData("ASSIGNED")]
        public void Validate_Should_Pass_When_TypeIsValid(string? type)
        {
            var query = new GetAllTasksQuery(Type: type);
            var result = _validator.Validate(query);
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Validate_Should_Fail_When_TypeIsInvalid()
        {
            var query = new GetAllTasksQuery(Type: "invalid_type");
            var result = _validator.Validate(query);
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == nameof(GetAllTasksQuery.Type));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("all")]
        [InlineData("ALL")]
        [InlineData("Todo")]
        [InlineData("InProgress")]
        [InlineData("InReview")]
        [InlineData("Done")]
        [InlineData("Cancelled")]
        public void Validate_Should_Pass_When_StatusIsValid(string? status)
        {
            var query = new GetAllTasksQuery(Status: status);
            var result = _validator.Validate(query);
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Validate_Should_Fail_When_StatusIsInvalid()
        {
            var query = new GetAllTasksQuery(Status: "NotAStatus");
            var result = _validator.Validate(query);
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == nameof(GetAllTasksQuery.Status));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("all")]
        [InlineData("ALL")]
        [InlineData("Low")]
        [InlineData("Medium")]
        [InlineData("High")]
        [InlineData("Critical")]
        public void Validate_Should_Pass_When_PriorityIsValid(string? priority)
        {
            var query = new GetAllTasksQuery(Priority: priority);
            var result = _validator.Validate(query);
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Validate_Should_Fail_When_PriorityIsInvalid()
        {
            var query = new GetAllTasksQuery(Priority: "SuperUrgent");
            var result = _validator.Validate(query);
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == nameof(GetAllTasksQuery.Priority));
        }

        [Fact]
        public void Validate_Should_Pass_When_WorkspaceIdIsNull()
        {
            var query = new GetAllTasksQuery(WorkspaceId: null);
            var result = _validator.Validate(query);
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Validate_Should_Pass_When_WorkspaceIdIsValidGuid()
        {
            var query = new GetAllTasksQuery(WorkspaceId: Guid.NewGuid());
            var result = _validator.Validate(query);
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Validate_Should_Fail_When_WorkspaceIdIsEmptyGuid()
        {
            var query = new GetAllTasksQuery(WorkspaceId: Guid.Empty);
            var result = _validator.Validate(query);
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == nameof(GetAllTasksQuery.WorkspaceId));
        }
    }
}
