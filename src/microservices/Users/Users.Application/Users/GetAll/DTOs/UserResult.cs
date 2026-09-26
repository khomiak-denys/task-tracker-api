namespace Users.Application.DTOs
{
    public record UserResult(Guid Id, string Email, string UserName, string? FullName);
}
