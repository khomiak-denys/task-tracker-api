using System.Text.Json;
using FluentAssertions;
using Users.API.Users.Requests;
using Xunit;

namespace Users.API.Tests.Requests
{
    public class GetUsersBatchRequestJsonConverterTests
    {
        [Fact]
        public void Deserialize_Should_ParseUserIds_When_PayloadIsJsonObjectWithUserIds()
        {
            // Arrange
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            var json = $"{{\"userIds\":[\"{id1}\",\"{id2}\"]}}";

            // Act
            var result = JsonSerializer.Deserialize<GetUsersBatchRequest>(json);

            // Assert
            result.Should().NotBeNull();
            result!.UserIds.Should().HaveCount(2);
            result.UserIds.Should().Contain(id1);
            result.UserIds.Should().Contain(id2);
        }

        [Fact]
        public void Deserialize_Should_ParseUserIds_When_PayloadIsJsonArray()
        {
            // Arrange
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            var json = $"[\"{id1}\",\"{id2}\"]";

            // Act
            var result = JsonSerializer.Deserialize<GetUsersBatchRequest>(json);

            // Assert
            result.Should().NotBeNull();
            result!.UserIds.Should().HaveCount(2);
            result.UserIds.Should().Contain(id1);
            result.UserIds.Should().Contain(id2);
        }

        [Fact]
        public void Deserialize_Should_ReturnEmptyList_When_PayloadIsEmptyObject()
        {
            // Arrange
            var json = "{}";

            // Act
            var result = JsonSerializer.Deserialize<GetUsersBatchRequest>(json);

            // Assert
            result.Should().NotBeNull();
            result!.UserIds.Should().BeEmpty();
        }

        [Fact]
        public void Serialize_Should_WriteJsonObjectWithUserIds_When_Serializing()
        {
            // Arrange
            var id = Guid.NewGuid();
            var request = new GetUsersBatchRequest(new[] { id });

            // Act
            var json = JsonSerializer.Serialize(request);

            // Assert
            json.Should().Contain("\"userIds\":");
            json.Should().Contain(id.ToString());
        }
    }
}
