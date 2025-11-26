using Application.Features.Plans.Queries.GetAllPlans;
using Application.Features.Plans.Queries.GetPlanById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlansController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PlansController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetPlans(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllPlansQuery(), cancellationToken);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlanById(int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetPlanByIdQuery(id), cancellationToken);
            return result.Match<IActionResult>(
                plan => Ok(plan),
                error => StatusCode((int)error.StatusCode, error.Message)
            );
        }

    }
}
