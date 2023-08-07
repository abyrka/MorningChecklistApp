using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MorningChecklist.Infrastructure.Entities;

namespace MorningChecklist.Infrastructure.Data
{
    public static class DatabaseSeeder
    {
        public static void SeedData(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (!context.Users.Any() || !context.MorningChecklistItems.Any() || !context.UserMorningChecklists.Any())
                {
                    var item1 = new MorningChecklistItemEntity { Title = "Item 1" };
                    var item2 = new MorningChecklistItemEntity { Title = "Item 2" };
                    context.MorningChecklistItems.AddRange(item1, item2);

                    context.SaveChanges();

                    var team1 = new TeamEntity { Name = "Development Team" };
                    var team2 = new TeamEntity { Name = "PM Team" };
                    context.Teams.AddRange(team1, team2);

                    context.SaveChanges();

                    var user1 = new UserEntity { Name = "John", TeamId = team1.Id };
                    var user2 = new UserEntity { Name = "Sam", TeamId = team1.Id };
                    var user3 = new UserEntity { Name = "Ann", TeamId = team2.Id };
                    var user4 = new UserEntity { Name = "Mark", TeamId = team2.Id };
                    context.Users.AddRange(user1, user2, user3, user4);

                    context.SaveChanges();

                    var userMorningChecklist1 = new UserMorningChecklistEntity { UserId = user1.Id, MorningChecklistItemId = item1.Id, IsDone = false, CreatedAt = DateTime.UtcNow.AddDays(-1) };
                    var userMorningChecklist2 = new UserMorningChecklistEntity { UserId = user1.Id, MorningChecklistItemId = item2.Id, IsDone = false, CreatedAt = DateTime.UtcNow.AddDays(-1) };
                    var userMorningChecklist3 = new UserMorningChecklistEntity { UserId = user2.Id, MorningChecklistItemId = item1.Id, IsDone = false, CreatedAt = DateTime.UtcNow.AddDays(-1) };
                    var userMorningChecklist4 = new UserMorningChecklistEntity { UserId = user2.Id, MorningChecklistItemId = item2.Id, IsDone = false, CreatedAt = DateTime.UtcNow };
                    var userMorningChecklist5 = new UserMorningChecklistEntity { UserId = user3.Id, MorningChecklistItemId = item1.Id, IsDone = true, CreatedAt = DateTime.UtcNow };
                    var userMorningChecklist6 = new UserMorningChecklistEntity { UserId = user3.Id, MorningChecklistItemId = item2.Id, IsDone = true, CreatedAt = DateTime.UtcNow.AddDays(-1) };
                    var userMorningChecklist7 = new UserMorningChecklistEntity { UserId = user4.Id, MorningChecklistItemId = item1.Id, IsDone = true, CreatedAt = DateTime.UtcNow.AddDays(-1) };
                    var userMorningChecklist8 = new UserMorningChecklistEntity { UserId = user4.Id, MorningChecklistItemId = item2.Id, IsDone = true, CreatedAt = DateTime.UtcNow.AddDays(-1) };
                    context.UserMorningChecklists.AddRange(userMorningChecklist1, userMorningChecklist2, userMorningChecklist3, userMorningChecklist4,
                        userMorningChecklist5, userMorningChecklist6, userMorningChecklist7, userMorningChecklist8);

                    context.SaveChanges();
                }
            }
        }
    }
}
