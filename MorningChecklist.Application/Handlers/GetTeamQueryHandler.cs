using AutoMapper;
using MediatR;
using MorningChecklist.Application.Queries;
using MorningChecklist.Application.Services.Interfaces;
using MorningChecklist.Domain.Models;

namespace MorningChecklist.Application.Handlers
{
    public class GetTeamQueryHandler : IRequestHandler<GetTeamQuery, TeamModel>
    {
        private readonly IMapper _mapper;
        private readonly ITeamService _teamService;

        public GetTeamQueryHandler(IMapper mapper,
            ITeamService teamService)
        {
            _mapper = mapper;
            _teamService = teamService;
        }

        public async Task<TeamModel> Handle(GetTeamQuery request, CancellationToken cancellationToken)
        {
            var team = _teamService.GetTeamInfo(request.TeamId);
            if (team == null)
            {
                throw new Exception($"Team not found with ID {request.TeamId}");
            }
            var activeItems = team.Users
                    .Where(item => item.UserMorningChecklists
                        .Any(checklist => !checklist.IsDone || (checklist.IsDone && checklist.CreatedAt.Date != DateTime.Today)));

            var model = _mapper.Map<TeamModel>(team);
            model.IsGoodToGo = activeItems.Count() == 0;
            return model;
        }
    }
}
