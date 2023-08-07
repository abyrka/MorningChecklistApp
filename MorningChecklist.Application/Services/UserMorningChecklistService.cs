using MorningChecklist.Application.Services.Interfaces;
using MorningChecklist.Infrastructure.Entities;
using MorningChecklist.Infrastructure.Repositories.Interfaces;

namespace MorningChecklist.Application.Services
{
    public class UserMorningChecklistService : IUserMorningChecklistService
    {
        private readonly IRepository<UserMorningChecklistEntity> _repository;

        public UserMorningChecklistService(IRepository<UserMorningChecklistEntity> repository)
        {
            _repository = repository;
        }

        public void CompleteUserMorningChecklistItem(int userId, int morningChecklistItemId)
        {
            var userMorningChecklist = _repository.Get(item => item.UserId == userId && item.MorningChecklistItemId == morningChecklistItemId)
                .SingleOrDefault();
            if (userMorningChecklist == null)
            {
                throw new Exception($"User MorningChecklist Item not found with UserID {userId} and ItemID {morningChecklistItemId}");
            }

            userMorningChecklist.IsDone = true;
            userMorningChecklist.CreatedAt = DateTime.UtcNow;
            _repository.Update(userMorningChecklist);
        }
    }
}
