using Application.Features.Subscriptions.Commands.CancelSubscription;
using Application.Features.Subscriptions.Commands.CreatePaymentWebHook;
using Application.Features.Subscriptions.Commands.CreateSubscription;
using Application.Features.Subscriptions.Queries.GetCurrentSubscription;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SubscriptionController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SubscriptionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("current")]
        public async Task<IActionResult> GetCurrent(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetCurrentSubscriptionQuery(), cancellationToken);
            return result.Match<IActionResult>(
                data => Ok(data),
                error => StatusCode((int)error.StatusCode, error.Message)
            );
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubscription(CreateSubscriptionCommand createSubscriptionCommand, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(createSubscriptionCommand, cancellationToken);
            return result.Match<IActionResult>(
                subscription => CreatedAtAction(nameof(CreateSubscription), subscription),
                error => StatusCode((int)error.StatusCode, error.Message)
            );
        }

        [HttpPost("webhook")]
        [AllowAnonymous]
        public async Task<IActionResult> HandleWebhook([FromQuery] string Hmac, CreatePaymentWebHookCommand createPaymentWebHookCommand,
            CancellationToken cancellationToken)
        {
            createPaymentWebHookCommand = createPaymentWebHookCommand with { Hmac = Hmac };
            var result = await _mediator.Send(createPaymentWebHookCommand, cancellationToken);
            return result.Match<IActionResult>(
                _ => Ok(),
                error => StatusCode((int)error.StatusCode, error.Message)
            );
        }

        [HttpPut]
        public async Task<IActionResult> CancelSubscription(CancelSubscriptionCommand cancelSubscriptionCommand, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(cancelSubscriptionCommand, cancellationToken);
            return result.Match<IActionResult>(
                _ => NoContent(),
                error => StatusCode((int)error.StatusCode, error.Message)
            );
        }

        [HttpPost("change-plan")]
        public async Task<IActionResult> ChangeSubscriptionPlan(CreateSubscriptionCommand createSubscriptionCommand, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(createSubscriptionCommand, cancellationToken);
            return result.Match<IActionResult>(
                subscription => CreatedAtAction(nameof(CreateSubscription), subscription),
                error => StatusCode((int)error.StatusCode, error.Message)
            );
        }
    }
}
