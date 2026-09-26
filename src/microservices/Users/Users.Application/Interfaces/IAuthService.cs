using DomainFramework.Results;
using Users.Application.DTOs;

namespace Users.Application.Interfaces
{
    public interface IAuthService
    {
        Task<Result<AuthTokensResult>> RegisterAsync(string email, string userName, string password, CancellationToken ct);
        Task<Result<AuthTokensResult>> LoginAsync(string email, string password, CancellationToken ct);
        Task<Result<AuthTokensResult>> RefreshTokenAsync(string accessToken, string refreshToken, CancellationToken ct);
        Task<Result> RevokeRefreshTokenAsync(Guid userId, CancellationToken ct);
    }
}
