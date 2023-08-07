using MediatR;
using Microsoft.AspNetCore.Mvc;
using MorningChecklist.Application.Queries;
using MorningChecklist.Application.Commands;
using MorningChecklist.Domain.Models;

namespace MorningChecklist.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<UserModel>> Get(int userId)
        {
            var query = new GetUserQuery { UserId = userId };
            var userChecklist = await _mediator.Send(query);
            return Ok(userChecklist);
        }

        [HttpPost("{userId}/mark-item")]
        public async Task<ActionResult> CompleteMorningChecklistItem([FromBody] int checklistItemId, int userId)
        {
            var command = new CompleteMorningChecklistItemCommand { UserId = userId, MorningChecklistItemId = checklistItemId };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
