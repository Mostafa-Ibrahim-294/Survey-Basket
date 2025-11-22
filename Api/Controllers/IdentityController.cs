using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Application.Features.Users.Commands.ChangePassword;
using Application.Features.Users.Commands.ConfirmEmail;
using Application.Features.Users.Commands.ForgetPassword;
using Application.Features.Users.Commands.Login;
using Application.Features.Users.Commands.Refresh;
using Application.Features.Users.Commands.Register;
using Application.Features.Users.Commands.ResendConfirmationEmail;
using Application.Features.Users.Commands.ResetPassword;
using Application.Features.Users.Commands.UpdateProfile;
using Application.Features.Users.Queries.GetUserProfile;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Infrastructure.Constants;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting(ServiceConstants.UserLimiterPolicy)]
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
        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> Profile(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetUserProfileQuery(), cancellationToken);
            return Ok(result);
        }
        [HttpPut("update-profile")]
        [Authorize]
        public async Task<IActionResult> UpdateProfile(UpdateProfileCommand updateProfileCommand, CancellationToken cancellationToken)
        {
            await _mediator.Send(updateProfileCommand, cancellationToken);
            return NoContent();
        }
        [HttpPut("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordCommand changePasswordCommand, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(changePasswordCommand, cancellationToken);
            return result.Match<IActionResult>(
                success => NoContent(),
                error => StatusCode((int)error.StatusCode, error.Message)
            );
        }
        [HttpPost("forget-password")]
        public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswordCommand forgetPasswordCommand, CancellationToken cancellationToken)
        {
            await _mediator.Send(forgetPasswordCommand, cancellationToken);
            return NoContent();
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand resetPasswordCommand, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(resetPasswordCommand, cancellationToken);
            return result.Match<IActionResult>(
                success => NoContent(),
                error => StatusCode((int)error.StatusCode, error.Message)
            );
        }
    }
}
