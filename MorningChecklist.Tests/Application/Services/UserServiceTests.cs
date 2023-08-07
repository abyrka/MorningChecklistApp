using FluentAssertions;
using Moq;
using MorningChecklist.Application.Services;
using MorningChecklist.Infrastructure.Entities;
using MorningChecklist.Infrastructure.Repositories.Interfaces;
using System.Linq.Expressions;
using Xunit;

namespace MorningChecklist.Tests.Application.Services
{
    public class UserServiceTests
    {
        [Fact]
        public void GetUserInfo_ValidUserId_ReturnsUserEntityWithAssociations()
        {
            // Arrange
            int userId = 1;
            var repositoryMock = new Mock<IRepository<UserEntity>>();
            var service = new UserService(repositoryMock.Object);

            var morningChecklistItem = new MorningChecklistItemEntity();
            var userMorningChecklistEntity = new UserMorningChecklistEntity { MorningChecklistItem = morningChecklistItem };
            var team = new TeamEntity();
            var userEntity = new UserEntity { Id = userId, Team = team, UserMorningChecklists = new[] { userMorningChecklistEntity } };

            var userEntities = new[] { userEntity }.AsQueryable();
            repositoryMock.Setup(repo => repo.Get(It.IsAny<Expression<Func<UserEntity, bool>>>()))
                .Returns((Expression<Func<UserEntity, bool>> predicate) => userEntities.Where(predicate));

            // Act
            var result = service.GetUserInfo(userId);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(userId);
            result.Team.Should().NotBeNull();
            result.UserMorningChecklists.Should().NotBeNull().And.HaveCount(1);
            result.UserMorningChecklists.First().MorningChecklistItem.Should().NotBeNull();
        }
    }
}
