using Application.Features.Polls.Commands.Create;
using Application.Features.Polls.Commands.Update;
using Application.Features.Polls.Queries.GetById;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PollsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {

        }
        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] GetByIdQuery getByIdQuery)
        {

        }
        [HttpPost]
        public IActionResult Create([FromBody] CreateCommand command)
        {
        }
        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateCommand command)
        {
        }
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
        }
    }
}
