using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MorningChecklist.Application.Queries;
using MorningChecklist.Controllers;
using MorningChecklist.Domain.Models;
using Xunit;
using MorningChecklist.Application.Commands;

namespace MorningChecklist.Tests.Controllers
{
    public class UserControllerTests
    {
        [Fact]
        public async Task Get_ValidUserId_ReturnsOkResultWithUserChecklist()
        {
            // Arrange
            int userId = 1;
            var mediatorMock = new Mock<IMediator>();
            var expectedUserChecklist = new UserModel { Id = userId, Name = "Test User" };
            mediatorMock.Setup(m => m.Send(It.IsAny<GetUserQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedUserChecklist);
            var controller = new UserController(mediatorMock.Object);

            // Act
            var response = await controller.Get(userId);

            // Assert
            var objectResponse = Assert.IsType<ActionResult<UserModel>>(response);
            var result = objectResponse.Result as OkObjectResult;
            Assert.Equal(200, result.StatusCode);

            var user = Assert.IsType<UserModel>(result.Value);
            user.Id.Should().Be(userId);
            user.Name.Should().Be("Test User");
        }

        [Fact]
        public async Task CompleteMorningChecklistItem_ValidData_ReturnsNoContent()
        {
            // Arrange
            int userId = 1;
            int checklistItemId = 123;
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(m => m.Send(It.IsAny<CompleteMorningChecklistItemCommand>(), It.IsAny<CancellationToken>())).Verifiable();
            var controller = new UserController(mediatorMock.Object);

            // Act
            var result = await controller.CompleteMorningChecklistItem(checklistItemId, userId);

            // Assert
            result.Should().BeOfType<NoContentResult>();
            mediatorMock.Verify(m => m.Send(It.IsAny<CompleteMorningChecklistItemCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
