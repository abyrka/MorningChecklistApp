using MorningChecklist.Infrastructure.Entities;

namespace MorningChecklist.Application.Services.Interfaces
{
    public interface IUserService
    {
        UserEntity GetUserInfo(int id);
    }
}
