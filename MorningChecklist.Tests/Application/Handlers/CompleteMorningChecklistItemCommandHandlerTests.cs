using Moq;
using MorningChecklist.Application.Commands;
using MorningChecklist.Application.Handlers;
using MorningChecklist.Application.Services.Interfaces;
using Xunit;

namespace MorningChecklist.Tests.Application.Handlers
{
    public class CompleteMorningChecklistItemCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ValidData_CallsCompleteUserMorningChecklistItem()
        {
            // Arrange
            int userId = 1;
            int checklistItemId = 123;
            var userMorningChecklistServiceMock = new Mock<IUserMorningChecklistService>();
            var handler = new CompleteMorningChecklistItemCommandHandler(userMorningChecklistServiceMock.Object);
            var command = new CompleteMorningChecklistItemCommand { UserId = userId, MorningChecklistItemId = checklistItemId };

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            userMorningChecklistServiceMock.Verify(service => service.CompleteUserMorningChecklistItem(userId, checklistItemId), Times.Once);
        }
    }
}
