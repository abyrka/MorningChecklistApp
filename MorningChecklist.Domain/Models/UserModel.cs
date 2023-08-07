namespace MorningChecklist.Domain.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int TeamId { get; set; }

        public TeamUserModel Team { get; set; }

        public List<UserMorningChecklistModel> MorningChecklists { get; set; }
    }
}
