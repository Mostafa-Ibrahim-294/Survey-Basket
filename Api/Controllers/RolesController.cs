using Application.Features.Roles.Commands.Create;
using Application.Features.Roles.Commands.Update;
using Application.Features.Roles.Queries.GetAll;
using Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Roles.Admin)]
    public class RolesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RolesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var roles = await _mediator.Send(new GetAllRolesQuery(), cancellationToken);
            return Ok(roles);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateRoleCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result.Match<IActionResult>(
                roleDto => CreatedAtAction(nameof(GetAll), roleDto),
                error => StatusCode((int)error.StatusCode, error.Message)
            );
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromRoute] string Id, [FromBody] UpdateRoleCommand command, CancellationToken cancellationToken)
        {
             command = command with { Id = Id };
            var result = await _mediator.Send(command, cancellationToken);
            return result.Match<IActionResult>(
                roleDto => Ok(roleDto),
                error => StatusCode((int)error.StatusCode, error.Message)
            );
        }
    }
}
