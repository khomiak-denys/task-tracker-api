using System.Text.Json.Serialization;

namespace Users.API.Users.Requests
{
    /// <summary>
    /// Request contract for retrieving contact information for a batch of users.
    /// </summary>
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
        [JsonConstructor]
        public GetUsersBatchRequest(IReadOnlyList<Guid>? userIds)
        {
            UserIds = userIds ?? Array.Empty<Guid>();
        }
    }
}
