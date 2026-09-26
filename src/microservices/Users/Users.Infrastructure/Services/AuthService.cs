using DomainFramework.Errors;
using DomainFramework.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using ServiceDefaults.Authentification;
using Users.Application.DTOs;
using Users.Application.Interfaces;
using Users.Infrastructure.Identity;

namespace Users.Infrastructure.Services
{
    internal sealed class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtTokenService _tokenService;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IOptions<JwtOptions> jwtOptions)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = new JwtTokenService(jwtOptions);
        }

        public async Task<Result<AuthTokensResult>> RegisterAsync(string email, string userName, string password, CancellationToken ct)
        {
            var existingUser = await _userManager.FindByEmailAsync(email);
            if (existingUser != null)
            {
                return Result<AuthTokensResult>.Failure(new AlreadyExistsError("User with this email already exists"));
            }

            var user = new ApplicationUser
            {
                Email = email,
                UserName = userName
            };

            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                return Result<AuthTokensResult>.Failure(new InvalidArgumentError(string.Join(", ", result.Errors.Select(e => e.Description))));
            }

            return await GenerateTokensAndUpdateUserAsync(user);
        }

        public async Task<Result<AuthTokensResult>> LoginAsync(string email, string password, CancellationToken ct)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return Result<AuthTokensResult>.Failure(new UnauthorizedError("Invalid email or password"));
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            if (!result.Succeeded)
            {
                return Result<AuthTokensResult>.Failure(new UnauthorizedError("Invalid email or password"));
            }

            return await GenerateTokensAndUpdateUserAsync(user);
        }

        public async Task<Result<AuthTokensResult>> RefreshTokenAsync(string accessToken, string refreshToken, CancellationToken ct)
        {
            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
            if (principal == null)
            {
                return Result<AuthTokensResult>.Failure(new UnauthorizedError("Invalid access token"));
            }

            var userName = principal.Identity?.Name;
            if (userName == null)
            {
                return Result<AuthTokensResult>.Failure(new UnauthorizedError("Invalid access token"));
            }

            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return Result<AuthTokensResult>.Failure(new UnauthorizedError("Invalid refresh token"));
            }

            var tokenValue = await _userManager.GetAuthenticationTokenAsync(user, "Application", "RefreshToken");
            if (tokenValue == null)
            {
                return Result<AuthTokensResult>.Failure(new UnauthorizedError("Invalid refresh token"));
            }

            var parts = tokenValue.Split('|');
            if (parts.Length != 2 || parts[0] != refreshToken || DateTime.Parse(parts[1], System.Globalization.CultureInfo.InvariantCulture) <= DateTime.UtcNow)
            {
                return Result<AuthTokensResult>.Failure(new UnauthorizedError("Invalid refresh token"));
            }

            return await GenerateTokensAndUpdateUserAsync(user);
        }

        public async Task<Result> RevokeRefreshTokenAsync(Guid userId, CancellationToken ct)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return Result.Failure(new NotFoundError("User not found"));
            }

            await _userManager.RemoveAuthenticationTokenAsync(user, "Application", "RefreshToken");

            return Result.Success();
        }

        private async Task<Result<AuthTokensResult>> GenerateTokensAndUpdateUserAsync(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var tokens = _tokenService.GenerateTokens(user, roles);

            await _userManager.SetAuthenticationTokenAsync(user, "Application", "RefreshToken", tokens.RefreshToken);

            return Result<AuthTokensResult>.Success(tokens);
        }
    }
}
