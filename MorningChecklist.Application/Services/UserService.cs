using Microsoft.EntityFrameworkCore;
using MorningChecklist.Application.Services.Interfaces;
using MorningChecklist.Infrastructure.Entities;
using MorningChecklist.Infrastructure.Repositories.Interfaces;

namespace MorningChecklist.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<UserEntity> _repository;

        public UserService(IRepository<UserEntity> repository)
        {
            _repository = repository;
        }

        public UserEntity GetUserInfo(int id)
        {
            var user = _repository.Get(item => item.Id == id)
                .Include(data => data.Team)
                .Include(data => data.UserMorningChecklists)
                    .ThenInclude(user => user.MorningChecklistItem)
                .SingleOrDefault();
            return user;
        }
    }
}
