using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceDefaults.Authorization;
using ServiceDefaults.ErrorHandling;
using Swashbuckle.AspNetCore.Annotations;
using Users.Application.DTOs;
using Users.Application.Users.GetContactInfo;

namespace Users.API.PrivateIntegrations.Users
{
    /// <summary>
    /// Controller providing internal integration endpoints for other microservices.
    /// </summary>
    [Route("api/v1/private-integrations/users")]
    [ApiController]
    [Authorize(Policy = Policies.IntegrationApiKey)]
    public class UsersPrivateIntegrationsController : ControllerBase
    {
        private readonly ISender _sender;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersPrivateIntegrationsController"/> class.
        /// </summary>
        /// <param name="sender">The MediatR sender instance.</param>
        public UsersPrivateIntegrationsController(ISender sender)
        {
            _sender = sender;
        }

        /// <summary>
        /// Retrieves user contact information by unique identifier for internal service integrations.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <param name="ct">The cancellation token.</param>
        /// <returns>The user contact info if found; otherwise, 404 Not Found.</returns>
        [HttpGet("{id:guid}/contact-info")]
        [SwaggerOperation(Summary = "Gets user contact info by id for private integrations")]
        [ProducesResponseType(typeof(UserContactInfoResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetContactInfo(Guid id, CancellationToken ct = default)
        {
            var query = new GetUserContactInfoQuery(id);
            var result = await _sender.Send(query, ct);

            return result.Match(Ok(result.Value), error => this.ToActionResult(error));
        }
    }
}
