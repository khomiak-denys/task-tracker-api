using FluentAssertions;
using Users.Application.Users.GetContactInfoBatch;
using Xunit;

namespace Users.Application.Tests.Users.GetContactInfoBatch
{
    public class GetUsersBatchQueryValidatorTests
    {
        private readonly GetUsersBatchQueryValidator _validator = new();

        [Fact]
        public void Validate_Should_Pass_When_ValidInput()
        {
            // Arrange
            var query = new GetUsersBatchQuery(new[] { Guid.NewGuid() }, Page: 1, PageSize: 10);

            // Act
            var result = _validator.Validate(query);

            // Assert
            result.IsValid.Should().BeTrue();
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public void Validate_Should_Fail_When_UserIdsIsNull()
        {
            // Arrange
            var query = new GetUsersBatchQuery(null!, Page: 1, PageSize: 10);

            // Act
            var result = _validator.Validate(query);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == nameof(GetUsersBatchQuery.UserIds));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Validate_Should_Fail_When_PageIsLessThanOne(int page)
        {
            // Arrange
            var query = new GetUsersBatchQuery(new[] { Guid.NewGuid() }, Page: page, PageSize: 10);

            // Act
            var result = _validator.Validate(query);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == nameof(GetUsersBatchQuery.Page));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        public void Validate_Should_Fail_When_PageSizeIsZeroOrNegative(int pageSize)
        {
            // Arrange
            var query = new GetUsersBatchQuery(new[] { Guid.NewGuid() }, Page: 1, PageSize: pageSize);

            // Act
            var result = _validator.Validate(query);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == nameof(GetUsersBatchQuery.PageSize));
        }

        [Fact]
        public void Validate_Should_Fail_When_PageSizeExceedsMaximum()
        {
            // Arrange
            var query = new GetUsersBatchQuery(new[] { Guid.NewGuid() }, Page: 1, PageSize: 101);

            // Act
            var result = _validator.Validate(query);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == nameof(GetUsersBatchQuery.PageSize));
        }
    }
}
