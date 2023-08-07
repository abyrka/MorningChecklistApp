using MediatR;
using Microsoft.AspNetCore.Mvc;
using MorningChecklist.Application.Queries;
using MorningChecklist.Domain.Models;

namespace MorningChecklist.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TeamController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{teamId}")]
        public async Task<ActionResult<TeamModel>> Get(int teamId)
        {
            var query = new GetTeamQuery { TeamId = teamId };
            var team = await _mediator.Send(query);
            return Ok(team);
        }
    }
}
