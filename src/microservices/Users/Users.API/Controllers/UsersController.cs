using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceDefaults.ErrorHandling;
using Users.API.Contracts;
using Users.Application.Users.ChangePassword;
using Users.Application.Users.Delete;
using Users.Application.Users.GetAll;
using Users.Application.Users.GetProfile;
using Users.Application.Users.UpdateProfile;

namespace Users.API.Controllers
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
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            var query = new GetProfileQuery(id);
            var result = await _sender.Send(query, ct);

            return result.Match(Ok(result.Value), error => this.ToActionResult(error));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10, CancellationToken ct = default)
        {
            var query = new GetAllQuery(page, pageSize);
            var result = await _sender.Send(query, ct);

            return result.Match(Ok(result.Value), error => this.ToActionResult(error));
        }

        [HttpPut("{id:guid}/profile")]
        public async Task<IActionResult> UpdateProfile(Guid id, [FromBody] UpdateProfileRequest request, CancellationToken ct)
        {
            var command = new UpdateProfileCommand(id, request.FullName, request.UserName);
            var result = await _sender.Send(command, ct);

            return result.Match(NoContent(), error => this.ToActionResult(error));
        }

        [HttpPut("{id:guid}/password")]
        public async Task<IActionResult> ChangePassword(Guid id, [FromBody] ChangePasswordRequest request, CancellationToken ct)
        {
            var command = new ChangePasswordCommand(id, request.CurrentPassword, request.NewPassword);
            var result = await _sender.Send(command, ct);

            return result.Match(NoContent(), error => this.ToActionResult(error));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            var command = new DeleteCommand(id);
            var result = await _sender.Send(command, ct);

            return result.Match(NoContent(), error => this.ToActionResult(error));
        }
    }
}
