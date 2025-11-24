using Application.Features.Questions.Queries.GetAvailableQuestions;
using Application.Features.Votes.Commands.SaveVote;
using Domain.Constants;
using Infrastructure.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Api.Controllers
{
    [Route("api/polls/{pollId}/[controller]")]
    [ApiController]
    [Authorize(Roles = Roles.Member)]
    [EnableRateLimiting(ServiceConstants.ConcurrentLimiterPolicy)]
    public class VotesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public VotesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> Start([FromRoute]int pollId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAvailableQuestionsQuery(pollId), cancellationToken);
            return result.Match<IActionResult>(
                votes => Ok(votes),
                error => StatusCode((int)error.StatusCode, error.Message)
            );
        }
        [HttpPost]
        public async Task<IActionResult> Vote([FromRoute] int pollId, [FromBody] SaveVoteCommand command, CancellationToken cancellationToken)
        {
            command = command with { PollId = pollId };
             await _mediator.Send(command, cancellationToken);
            return Created();
        }
    }
}
