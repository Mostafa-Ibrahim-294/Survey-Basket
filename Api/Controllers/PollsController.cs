using System.Threading;
using System.Threading.Tasks;
using Application.Features.Polls.Commands.Create;
using Application.Features.Polls.Commands.Delete;
using Application.Features.Polls.Commands.Update;
using Application.Features.Polls.Dtos;
using Application.Features.Polls.Queries.GetAll;
using Application.Features.Polls.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            if (result is null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCommand createPollCommand , CancellationToken cancellationToken)
        {
            var created = await _mediator.Send(createPollCommand, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommand updatePollCommand, CancellationToken cancellationToken)
        {
            updatePollCommand = updatePollCommand with { id = id };
            var updated = await _mediator.Send(updatePollCommand, cancellationToken);
            if (!updated) return NotFound();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
        {
            var deleted = await _mediator.Send(new DeleteCommand(id), cancellationToken);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
