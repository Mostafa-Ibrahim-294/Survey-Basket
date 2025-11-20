using Application.Features.Users.Queries.GetAllUsers;
using Application.Features.Users.Queries.GetUser;
using Application.Features.Users.Commands.CreateUser;
using Application.Features.Users.Commands.UpdateUser;
using Application.Features.Users.Commands.UnlockUser;
using Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Roles.Admin)]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllUsersQuery(), cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] string id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetUserQuery(id), cancellationToken);
            return result.Match(
                user => Ok(user),
                error => StatusCode((int)error.StatusCode, error.Message)   
            );
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result.Match<IActionResult>(
                user => CreatedAtAction(nameof(GetUser), new { id = user.Id }, user),
                error => StatusCode((int)error.StatusCode, error.Message)
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdateUserCommand command, CancellationToken cancellationToken)
        {
            command = command with { Id = id };
            var result = await _mediator.Send(command, cancellationToken);
            return result.Match<IActionResult>(
                user => Ok(user),
                error => StatusCode((int)error.StatusCode, error.Message)
            );
        }

        [HttpPost("{id}/unlock")]
        public async Task<IActionResult> Unlock([FromRoute] string id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new UnlockUserCommand(id), cancellationToken);
            return result.Match<IActionResult>(
                user => Ok(user),
                error => StatusCode((int)error.StatusCode, error.Message)
            );
        }
    }
}
