using Users.Application.Users.ChangePassword;

namespace Users.API.Users.Requests
{
    public record ChangeUserPasswordRequest(string CurrentPassword, string NewPassword)
    {
        public ChangeUserPasswordCommand ToCommand(Guid userId)
        {
            return new ChangeUserPasswordCommand(userId, CurrentPassword, NewPassword);
        }
    }
}
