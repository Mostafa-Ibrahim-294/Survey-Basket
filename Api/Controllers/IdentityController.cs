using Application.Features.Users.Commands.Login;
using Application.Features.Users.Commands.Refresh;
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
        public async Task<IActionResult> Login(LoginCommand loginCommand , CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(loginCommand , cancellationToken);
            if (result is null) return BadRequest("Invalid");
            return Ok(result);
        }
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken(RefreshCommand refreshTokenCommand , CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(refreshTokenCommand , cancellationToken);
            if (result is null) return BadRequest("Invalid");
            return Ok(result);
        }
    }
}
