using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MorningChecklist.Infrastructure.Entities
{
    public class UserMorningChecklistEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int UserId { get; set; }

        public int MorningChecklistItemId { get; set; }

        public bool IsDone { get; set; }

        public DateTime CreatedAt { get; set; }

        public UserEntity User { get; set; }

        public MorningChecklistItemEntity MorningChecklistItem { get; set; }
    }
}
