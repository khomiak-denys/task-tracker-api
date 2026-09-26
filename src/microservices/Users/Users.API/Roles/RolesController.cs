using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceDefaults.ErrorHandling;
using Users.Application.Roles;
using Users.Application.Roles.Assign;
using Users.Application.Roles.GetAll;
using Users.Application.Roles.GetForUser;
using Users.Application.Roles.Remove;

namespace Users.API.Roles
{
    [Route("api/v1/roles")]
    [ApiController]
    [Authorize(Roles = nameof(AppRole.Admin))]
    public class RolesController : ControllerBase
    {
        private readonly ISender _sender;

        public RolesController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var query = new GetAllQuery();
            var result = await _sender.Send(query, ct);

            return result.Match(Ok(result.Value), error => this.ToActionResult(error));
        }

        [HttpPost("/api/users/{userId:guid}/roles/{role}")]
        public async Task<IActionResult> Assign(Guid userId, AppRole role, CancellationToken ct)
        {
            var command = new AssignCommand(userId, role);
            var result = await _sender.Send(command, ct);

            return result.Match(NoContent(), error => this.ToActionResult(error));
        }

        [HttpDelete("/api/users/{userId:guid}/roles/{role}")]
        public async Task<IActionResult> Remove(Guid userId, AppRole role, CancellationToken ct)
        {
            var command = new RemoveCommand(userId, role);
            var result = await _sender.Send(command, ct);

            return result.Match(NoContent(), error => this.ToActionResult(error));
        }

        [HttpGet("/api/users/{userId:guid}/roles")]
        public async Task<IActionResult> GetForUser(Guid userId, CancellationToken ct)
        {
            var query = new GetForUserQuery(userId);
            var result = await _sender.Send(query, ct);

            return result.Match(Ok(result.Value), error => this.ToActionResult(error));
        }
    }
}
