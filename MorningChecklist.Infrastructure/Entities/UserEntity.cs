using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MorningChecklist.Infrastructure.Entities
{
    public class UserEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public int TeamId { get; set; }

        public TeamEntity Team { get; set; }

        public ICollection<UserMorningChecklistEntity> UserMorningChecklists { get; set; }
    }
}
