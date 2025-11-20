using Application.Features.Users.Queries.GetAllUsers;
using Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Roles.Admin)]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllUsersQuery(), cancellationToken);
            return Ok(result);
        }
    }
}
