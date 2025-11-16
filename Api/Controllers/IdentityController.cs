using Application.Features.Users.Commands.ConfirmEmail;
using Application.Features.Users.Commands.Login;
using Application.Features.Users.Commands.Refresh;
using Application.Features.Users.Commands.Register;
using Application.Features.Users.Commands.ResendConfirmationEmail;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IMediator _mediator;
        public IdentityController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCommand loginCommand, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(loginCommand, cancellationToken);
            if (result is null) return BadRequest("Invalid");
            return Ok(result);
        }
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken(RefreshCommand refreshTokenCommand, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(refreshTokenCommand, cancellationToken);
            if (result is null) return BadRequest("Invalid");
            return Ok(result);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterCommand registerCommand, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(registerCommand, cancellationToken);
            if (!result) return BadRequest();
            return Ok();
        }
        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailCommand confirmEmailCommand, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(confirmEmailCommand, cancellationToken);
            if (!result) return BadRequest("Invalid");
            return Ok();
        }
        [HttpPost("resend-confirmation-email")]
        public async Task<IActionResult> ResendConfirmationEmail([FromQuery] ResendConfirmationEmailCommand resendConfirmationEmailCommand, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(resendConfirmationEmailCommand, cancellationToken);
            if (!result) return BadRequest();
            return Ok();
        }

    }
}
