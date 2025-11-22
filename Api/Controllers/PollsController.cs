using System.Threading;
using System.Threading.Tasks;
using Application.Features.Polls.Commands.Create;
using Application.Features.Polls.Commands.Delete;
using Application.Features.Polls.Commands.Update;
using Application.Features.Polls.Dtos;
using Application.Features.Polls.Queries.GetAll;
using Application.Features.Polls.Queries.GetById;
using Domain.Constants;
using Infrastructure.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Roles.Admin)]
    [EnableRateLimiting(ServiceConstants.ConcurrentLimiterPolicy)]
    public class PollsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PollsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllQuery(), cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetByIdQuery(id), cancellationToken);
            return result.Match<IActionResult>(
                poll => Ok(poll),
                error => StatusCode((int)error.StatusCode , error.Message)
            );
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCommand createPollCommand , CancellationToken cancellationToken)
        {
            var created = await _mediator.Send(createPollCommand, cancellationToken);
            return created.Match<IActionResult>(
                poll => CreatedAtAction(nameof(GetById), new { id = poll.Id }, poll),
                error => StatusCode((int)error.StatusCode , error.Message)
            );
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommand updatePollCommand, CancellationToken cancellationToken)
        {
            updatePollCommand = updatePollCommand with { id = id };
            var updated = await _mediator.Send(updatePollCommand, cancellationToken);
            return updated.Match<IActionResult>(
                _ => NoContent(),
                error => StatusCode((int)error.StatusCode , error.Message)
            );
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
        {
            var deleted = await _mediator.Send(new DeleteCommand(id), cancellationToken);
            return deleted.Match<IActionResult>(
                _ => NoContent(),
                error => StatusCode((int)error.StatusCode , error.Message)
            );
        }
    }
}
