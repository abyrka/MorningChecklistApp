using MediatR;
using MorningChecklist.Domain.Models;

namespace MorningChecklist.Application.Queries
{
    public class GetTeamQuery : IRequest<TeamModel>
    {
        public int TeamId { get; set; }
    }
}
