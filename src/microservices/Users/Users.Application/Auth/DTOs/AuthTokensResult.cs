namespace Users.Application.DTOs
{
    public record AuthTokensResult(string AccessToken, string RefreshToken, DateTime ExpiresAt);
}
