using Microsoft.EntityFrameworkCore;
using MorningChecklist.Application.Services.Interfaces;
using MorningChecklist.Infrastructure.Entities;
using MorningChecklist.Infrastructure.Repositories.Interfaces;

namespace MorningChecklist.Application.Services
{
    public class TeamService : ITeamService
    {
        private readonly IRepository<TeamEntity> _repository;

        public TeamService(IRepository<TeamEntity> repository)
        {
            _repository = repository;
        }

        public TeamEntity GetTeamInfo(int id)
        {
            var team = _repository.Get(item => item.Id == id)
                .Include(data => data.Users)
                    .ThenInclude(user => user.UserMorningChecklists)
                .SingleOrDefault();
            return team;
        }
    }
}
