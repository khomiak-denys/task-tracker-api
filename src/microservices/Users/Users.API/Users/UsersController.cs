using System.Security.Claims;
using DomainFramework;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceDefaults.ErrorHandling;
using Swashbuckle.AspNetCore.Annotations;
using Users.API.Users.Requests;
using Users.Application.DTOs;
using Users.Application.Roles;
using Users.Application.Users.ChangePassword;
using Users.Application.Users.Delete;
using Users.Application.Users.GetAll;
using Users.Application.Users.GetProfile;

namespace Users.API.Users
{
    [Route("api/v1/users")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly ISender _sender;

        public UsersController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("{id:guid}")]
        [SwaggerOperation(Summary = "Gets a user profile by ID")]
        [ProducesResponseType(typeof(UserProfileResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            var currentUserId = GetUserId();
            if (currentUserId == Guid.Empty)
            {
                return Unauthorized();
            }

            var isAdmin = User.IsInRole(nameof(AppRole.Admin));
            if (!isAdmin && currentUserId != id)
            {
                return Forbid();
            }

            var query = new GetProfileQuery(id);
            var result = await _sender.Send(query, ct);

            return result.Match(Ok(result.Value), error => this.ToActionResult(error));
        }

        [HttpGet]
        [Authorize(Roles = nameof(AppRole.Admin))]
        [SwaggerOperation(Summary = "Gets all users with pagination")]
        [ProducesResponseType(typeof(PaginationResult<UserResult>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10, CancellationToken ct = default)
        {
            var query = new GetAllQuery(page, pageSize);
            var result = await _sender.Send(query, ct);

            return result.Match(Ok(result.Value), error => this.ToActionResult(error));
        }

        [HttpPut("{id:guid}/profile")]
        [SwaggerOperation(Summary = "Updates user profile")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProfile(Guid id, [FromBody] UpdateProfileRequest request, CancellationToken ct)
        {
            var currentUserId = GetUserId();
            if (currentUserId == Guid.Empty)
            {
                return Unauthorized();
            }

            var isAdmin = User.IsInRole(nameof(AppRole.Admin));
            if (!isAdmin && currentUserId != id)
            {
                return Forbid();
            }

            var result = await _sender.Send(request.ToCommand(id), ct);

            return result.Match(NoContent(), error => this.ToActionResult(error));
        }

        [HttpPut("{id:guid}/password")]
        [SwaggerOperation(Summary = "Changes user password")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ChangePassword(Guid id, [FromBody] ChangePasswordRequest request, CancellationToken ct)
        {
            var currentUserId = GetUserId();
            if (currentUserId == Guid.Empty)
            {
                return Unauthorized();
            }

            if (currentUserId != id)
            {
                return Forbid();
            }

            var result = await _sender.Send(request.ToCommand(id), ct);

            return result.Match(NoContent(), error => this.ToActionResult(error));
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = nameof(AppRole.Admin))]
        [SwaggerOperation(Summary = "Deletes a user")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            var command = new DeleteCommand(id);
            var result = await _sender.Send(command, ct);

            return result.Match(NoContent(), error => this.ToActionResult(error));
        }

        private Guid GetUserId()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrWhiteSpace(claim) || !Guid.TryParse(claim, out var userId))
            {
                return Guid.Empty;
            }

            return userId;
        }
    }
}
