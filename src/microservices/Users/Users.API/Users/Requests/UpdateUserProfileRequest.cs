using Users.Application.Users.UpdateProfile;

namespace Users.API.Users.Requests
{
    public record UpdateUserProfileRequest(string? FullName, string? UserName)
    {
        public UpdateUserProfileCommand ToCommand(Guid userId)
        {
            return new UpdateUserProfileCommand(
                userId,
                string.IsNullOrWhiteSpace(FullName) ? null : FullName.Trim(),
                string.IsNullOrWhiteSpace(UserName) ? null : UserName.Trim());
        }
    }
}
