using Application.Features.Questions.Commands.CreateQuestion;
using Application.Features.Questions.Commands.ToggleQuestion;
using Application.Features.Questions.Queries.GetAllQuestions;
using Application.Features.Questions.Queries.GetQuestionById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/polls/{pollId}/[controller]")]
    [ApiController]
    [Authorize]
    public class QuestionsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public QuestionsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> CreateQuestion([FromRoute]int pollId, CreateQuestionCommand createQuestionCommand , CancellationToken cancellationToken)
        {
            createQuestionCommand = createQuestionCommand with { PollId = pollId };
            var result = await _mediator.Send(createQuestionCommand, cancellationToken);
            return result.Match<IActionResult>(
                question => CreatedAtAction(nameof(GetQuestion), new { id = question.Id , pollId = pollId }, question),
                error => StatusCode((int)error.StatusCode, error.Message)
            );
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestion([FromRoute]int pollId, [FromRoute]int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetQuestionByIdQuery(pollId, id), cancellationToken);
            return result.Match<IActionResult>(
                question => Ok(question),
                error => StatusCode((int)error.StatusCode, error.Message)
            );
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromRoute]int pollId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllQuestionsQuery(pollId), cancellationToken);
            return result.Match<IActionResult>(
                questions => Ok(questions),
                error => StatusCode((int)error.StatusCode, error.Message)
            );
        }
        [HttpPut("{id}/toggle")]
        public async Task<IActionResult> ToggleQuestion([FromRoute] int pollId, [FromRoute] int id, [FromBody] ToggleQuestionCommand toggleQuestionCommand, CancellationToken cancellationToken)
        {
            toggleQuestionCommand = toggleQuestionCommand with { PollId = pollId, Id = id };
            var result = await _mediator.Send(toggleQuestionCommand, cancellationToken);
            return result.Match<IActionResult>(
                question => NoContent(),
                error => StatusCode((int)error.StatusCode, error.Message)
            );
        }
    }
}
