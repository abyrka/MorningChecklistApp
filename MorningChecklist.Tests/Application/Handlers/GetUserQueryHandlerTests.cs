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
    public class GetUserQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ValidUserId_ReturnsUserModel()
        {
            // Arrange
            int userId = 1;
            var mapperMock = new Mock<IMapper>();
            var userServiceMock = new Mock<IUserService>();
            var handler = new GetUserQueryHandler(mapperMock.Object, userServiceMock.Object);
            var query = new GetUserQuery { UserId = userId };

            var user = new UserEntity { Id = userId };
            userServiceMock.Setup(service => service.GetUserInfo(userId)).Returns(user);
            mapperMock.Setup(m => m.Map<UserModel>(It.IsAny<UserEntity>())).Returns(new UserModel());

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull().And.BeOfType<UserModel>();
            userServiceMock.Verify(service => service.GetUserInfo(userId), Times.Once);
            mapperMock.Verify(m => m.Map<UserModel>(user), Times.Once);
        }
    }
}
