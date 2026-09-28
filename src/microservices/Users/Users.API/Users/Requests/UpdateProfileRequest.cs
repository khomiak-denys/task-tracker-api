using Users.Application.Users.UpdateProfile;

namespace Users.API.Users.Requests
{
    public record UpdateProfileRequest(string? FullName, string? UserName)
    {
        public UpdateProfileCommand ToCommand(Guid userId)
        {
            return new UpdateProfileCommand(userId, FullName, UserName);
        }
    }
}
