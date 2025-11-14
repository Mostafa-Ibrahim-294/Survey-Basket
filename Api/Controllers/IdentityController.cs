using Application.Features.Users.Commands;
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
        [HttpPost]
        public async Task<IActionResult> Login(LoginCommand loginCommand , CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(loginCommand , cancellationToken);
            if (result is null) return BadRequest("Invalid");
            return Ok(result);
        }
    }
}
