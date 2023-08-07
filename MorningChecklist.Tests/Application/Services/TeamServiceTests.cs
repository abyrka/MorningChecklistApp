using FluentAssertions;
using Moq;
using MorningChecklist.Application.Services;
using MorningChecklist.Infrastructure.Entities;
using MorningChecklist.Infrastructure.Repositories.Interfaces;
using System.Linq.Expressions;
using Xunit;

namespace MorningChecklist.Tests.Application.Services
{
    public class TeamServiceTests
    {
        [Fact]
        public void GetTeamInfo_ValidTeamId_ReturnsTeamEntityWithUsers()
        {
            // Arrange
            int teamId = 1;
            var repositoryMock = new Mock<IRepository<TeamEntity>>();
            var service = new TeamService(repositoryMock.Object);

            var userMorningChecklists = new List<UserMorningChecklistEntity> { new UserMorningChecklistEntity() };
            var users = new List<UserEntity> { new UserEntity { UserMorningChecklists = userMorningChecklists } };
            var teamEntity = new TeamEntity { Id = teamId, Users = users };

            var teamEntities = new List<TeamEntity> { teamEntity }.AsQueryable();
            repositoryMock.Setup(repo => repo.Get(It.IsAny<Expression<Func<TeamEntity, bool>>>()))
                .Returns((Expression<Func<TeamEntity, bool>> predicate) => teamEntities.Where(predicate));

            // Act
            var result = service.GetTeamInfo(teamId);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(teamId);
            result.Users.Should().NotBeNull().And.HaveCount(1);
            result.Users.First().UserMorningChecklists.Should().NotBeNull().And.HaveCount(1);
        }
    }
}
