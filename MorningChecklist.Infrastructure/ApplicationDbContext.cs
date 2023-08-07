using Microsoft.EntityFrameworkCore;
using MorningChecklist.Infrastructure.Entities;

namespace MorningChecklist.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<MorningChecklistItemEntity> MorningChecklistItems { get; set; }

        public DbSet<UserMorningChecklistEntity> UserMorningChecklists { get; set; }

        public DbSet<TeamEntity> Teams { get; set; }

        public DbSet<UserEntity> Users { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        
    }
}