using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MorningChecklist.Application.Queries;
using MorningChecklist.Controllers;
using MorningChecklist.Domain.Models;
using Xunit;

namespace MorningChecklist.Tests.Controllers
{
    public class TeamControllerTests
    {
        [Fact]
        public async Task Get_ValidTeamId_ReturnsOkResultWithTeam()
        {
            // Arrange
            int teamId = 1;
            var mediatorMock = new Mock<IMediator>();
            var expectedTeam = new TeamModel { Id = teamId, Name = "Test Team" };
            mediatorMock.Setup(m => m.Send(It.IsAny<GetTeamQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedTeam);

            var controller = new TeamController(mediatorMock.Object);

            // Act
            var response = await controller.Get(teamId);

            // Assert
            var objectResponse = Assert.IsType<ActionResult<TeamModel>>(response);
            var result = objectResponse.Result as OkObjectResult;
            Assert.Equal(200, result.StatusCode);

            var team = Assert.IsType<TeamModel>(result.Value);
            team.Id.Should().Be(teamId);
            team.Name.Should().Be("Test Team");
        }
    }
}
