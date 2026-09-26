using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceDefaults.ErrorHandling;
using Users.API.Contracts;
using Users.Application.Auth.Login;
using Users.Application.Auth.RefreshToken;
using Users.Application.Auth.Register;
using Users.Application.Auth.RevokeRefreshToken;

namespace Users.API.Controllers
{
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ISender _sender;

        public AuthController(ISender sender)
        {
            _sender = sender;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken ct)
        {
            var command = new RegisterCommand(request.Email, request.UserName, request.Password);
            var result = await _sender.Send(command, ct);

            if (result.IsSuccess)
            {
                AppendRefreshTokenCookie(result.Value.RefreshToken);
                return Ok(new { AccessToken = result.Value.AccessToken });
            }

            return this.ToActionResult(result.Error);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken ct)
        {
            var command = new LoginCommand(request.Email, request.Password);
            var result = await _sender.Send(command, ct);

            if (result.IsSuccess)
            {
                AppendRefreshTokenCookie(result.Value.RefreshToken);
                return Ok(new { AccessToken = result.Value.AccessToken });
            }

            return this.ToActionResult(result.Error);
        }

        [AllowAnonymous]
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken(CancellationToken ct)
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(refreshToken))
            {
                return Unauthorized();
            }

            var authorizationHeader = Request.Headers.Authorization.ToString();
            if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                return Unauthorized();
            }

            var accessToken = authorizationHeader["Bearer ".Length..].Trim();

            var command = new RefreshTokenCommand(accessToken, refreshToken);
            var result = await _sender.Send(command, ct);

            if (result.IsSuccess)
            {
                AppendRefreshTokenCookie(result.Value.RefreshToken);
                return Ok(new { AccessToken = result.Value.AccessToken });
            }

            return this.ToActionResult(result.Error);
        }

        [Authorize]
        [HttpPost("revoke")]
        public async Task<IActionResult> RevokeToken(CancellationToken ct)
        {
            // Extract user id from the authorized user claims
            var userIdString = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out var userId))
            {
                return Unauthorized();
            }

            var command = new RevokeRefreshTokenCommand(userId);
            var result = await _sender.Send(command, ct);

            if (result.IsSuccess)
            {
                ClearRefreshTokenCookie();
                return NoContent();
            }

            return this.ToActionResult(result.Error);
        }

        private void AppendRefreshTokenCookie(string refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }

        private void ClearRefreshTokenCookie()
        {
            Response.Cookies.Delete("refreshToken");
        }
    }
}
