namespace MorningChecklist.Application.Services.Interfaces
{
    public interface IUserMorningChecklistService
    {
        void CompleteUserMorningChecklistItem(int userId, int morningChecklistItemId);
    }
}
