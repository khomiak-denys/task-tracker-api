namespace Users.API.Contracts
{
    public record ChangePasswordRequest(string CurrentPassword, string NewPassword);
}
