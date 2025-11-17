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
            return result.Match<IActionResult>(
                authResponse => Ok(authResponse),
                error => StatusCode((int)error.StatusCode, error.Message)
            );
        }
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken(RefreshCommand refreshTokenCommand, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(refreshTokenCommand, cancellationToken);
            return result.Match<IActionResult>(
                authResponse => Ok(authResponse),
                error => StatusCode((int)error.StatusCode, error.Message)
            );
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterCommand registerCommand, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(registerCommand, cancellationToken);
            return result.Match<IActionResult>(
                success => Ok(),
                error => StatusCode((int)error.StatusCode, error.Message)
            );
        }
        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailCommand confirmEmailCommand, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(confirmEmailCommand, cancellationToken);
            return result.Match<IActionResult>(
                success => Ok(),
                error => StatusCode((int)error.StatusCode, error.Message)
            );
        }
        [HttpPost("resend-confirmation-email")]
        public async Task<IActionResult> ResendConfirmationEmail([FromQuery] ResendConfirmationEmailCommand resendConfirmationEmailCommand, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(resendConfirmationEmailCommand, cancellationToken);
            return result.Match<IActionResult>(
                success => Ok(),
                error => StatusCode((int)error.StatusCode, error.Message)
            );
        }

    }
}
