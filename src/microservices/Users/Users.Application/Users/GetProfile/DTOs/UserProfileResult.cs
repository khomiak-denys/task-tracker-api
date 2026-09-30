namespace Users.Application.DTOs
{
    /// <summary>
    /// Represents the user profile query result contract.
    /// </summary>
    public record UserProfileResult(
        Guid Id,
        string Email,
        string UserName,
        string? FullName,
        bool EmailConfirmed,
        bool TwoFactorEnabled,
        DateTimeOffset? LockoutEnd,
        bool LockoutEnabled,
        int AccessFailedCount,
        IReadOnlyList<string> Roles);
}
