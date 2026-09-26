using Users.Application.Users.ChangePassword;

namespace Users.API.Users.Requests
{
    public record ChangePasswordRequest(string CurrentPassword, string NewPassword)
    {
        public ChangePasswordCommand ToCommand(Guid userId)
        {
            return new ChangePasswordCommand(userId, CurrentPassword, NewPassword);
        }
    }
}
