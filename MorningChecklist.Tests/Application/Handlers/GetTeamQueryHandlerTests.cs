using AutoMapper;
using FluentAssertions;
using Moq;
using MorningChecklist.Application.Handlers;
using MorningChecklist.Application.Queries;
using MorningChecklist.Application.Services.Interfaces;
using MorningChecklist.Domain.Models;
using MorningChecklist.Infrastructure.Entities;
using Xunit;

namespace MorningChecklist.Tests.Application.Handlers
{
    public class GetTeamQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ValidTeamId_ReturnsTeamModel()
        {
            // Arrange
            int teamId = 1;
            var mapperMock = new Mock<IMapper>();
            var teamServiceMock = new Mock<ITeamService>();
            var handler = new GetTeamQueryHandler(mapperMock.Object, teamServiceMock.Object);
            var query = new GetTeamQuery { TeamId = teamId };

            var userChecklist1 = new UserMorningChecklistEntity { IsDone = true, CreatedAt = DateTime.Today.AddDays(-1) };
            var userChecklist2 = new UserMorningChecklistEntity { IsDone = false, CreatedAt = DateTime.Today };
            var user1 = new UserEntity { UserMorningChecklists = new List<UserMorningChecklistEntity> { userChecklist1, userChecklist2 } };
            var user2 = new UserEntity { UserMorningChecklists = new List<UserMorningChecklistEntity> { userChecklist2 } };
            var team = new TeamEntity { Id = teamId, Users = new List<UserEntity> { user1, user2 } };

            teamServiceMock.Setup(service => service.GetTeamInfo(teamId)).Returns(team);
            mapperMock.Setup(m => m.Map<TeamModel>(It.IsAny<TeamEntity>())).Returns(new TeamModel());

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull().And.BeOfType<TeamModel>();
            teamServiceMock.Verify(service => service.GetTeamInfo(teamId), Times.Once);
            mapperMock.Verify(m => m.Map<TeamModel>(team), Times.Once);
        }
    }
}
