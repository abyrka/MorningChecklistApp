namespace MorningChecklist.Domain.Models
{
    public class UserMorningChecklistModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int MorningChecklistItemId { get; set; }

        public bool IsDone { get; set; }

        public DateTime CreatedAt { get; set; }

        public MorningChecklistItemModel MorningChecklistItem { get; set; }

    }
}
