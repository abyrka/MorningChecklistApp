using MorningChecklist.Infrastructure.Entities;

namespace MorningChecklist.Application.Services.Interfaces
{
    public interface ITeamService
    {
        TeamEntity GetTeamInfo(int id);
    }
}
