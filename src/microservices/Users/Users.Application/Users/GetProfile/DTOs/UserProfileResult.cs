namespace Users.Application.DTOs
{
    public record UserProfileResult(Guid Id, string Email, string UserName, string? FullName, IReadOnlyList<string> Roles);
}
