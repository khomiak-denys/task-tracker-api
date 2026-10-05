using System.Text.Json;
using System.Text.Json.Serialization;

namespace Users.API.Users.Requests
{
    /// <summary>
    /// Request contract for retrieving contact information for a batch of users.
    /// </summary>
    [JsonConverter(typeof(GetUsersBatchRequestJsonConverter))]
    public record GetUsersBatchRequest
    {
        /// <summary>
        /// Gets the collection of user unique identifiers to look up.
        /// </summary>
        public IReadOnlyList<Guid> UserIds { get; init; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetUsersBatchRequest"/> record.
        /// </summary>
        public GetUsersBatchRequest()
        {
            UserIds = Array.Empty<Guid>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetUsersBatchRequest"/> record with specified user IDs.
        /// </summary>
        /// <param name="userIds">The list of user IDs.</param>
        public GetUsersBatchRequest(IReadOnlyList<Guid>? userIds)
        {
            UserIds = userIds ?? Array.Empty<Guid>();
        }
    }

    /// <summary>
    /// Custom JSON converter that allows deserializing either an object with a <c>userIds</c> property or a direct JSON array of GUIDs.
    /// </summary>
    public class GetUsersBatchRequestJsonConverter : JsonConverter<GetUsersBatchRequest>
    {
        /// <inheritdoc />
        public override GetUsersBatchRequest Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.StartArray)
            {
                var list = JsonSerializer.Deserialize<List<Guid>>(ref reader, options) ?? new List<Guid>();
                return new GetUsersBatchRequest(list);
            }

            if (reader.TokenType == JsonTokenType.StartObject)
            {
                using var document = JsonDocument.ParseValue(ref reader);
                if (document.RootElement.TryGetProperty("userIds", out var userIdsProp) ||
                    document.RootElement.TryGetProperty("UserIds", out userIdsProp))
                {
                    var list = userIdsProp.Deserialize<List<Guid>>(options) ?? new List<Guid>();
                    return new GetUsersBatchRequest(list);
                }

                return new GetUsersBatchRequest(Array.Empty<Guid>());
            }

            throw new JsonException("Expected JSON array or JSON object with 'userIds' property.");
        }

        /// <inheritdoc />
        public override void Write(Utf8JsonWriter writer, GetUsersBatchRequest value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("userIds");
            JsonSerializer.Serialize(writer, value.UserIds, options);
            writer.WriteEndObject();
        }
    }
}
