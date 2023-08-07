using FluentAssertions;
using Moq;
using MorningChecklist.Application.Services;
using MorningChecklist.Infrastructure.Entities;
using MorningChecklist.Infrastructure.Repositories.Interfaces;
using System.Linq.Expressions;
using Xunit;

namespace MorningChecklist.Tests.Application.Services
{
    public class UserMorningChecklistServiceTests
    {
        [Fact]
        public void CompleteUserMorningChecklistItem_ValidData_CompletesAndUpdatesItem()
        {
            // Arrange
            int userId = 1;
            int morningChecklistItemId = 123;
            var repositoryMock = new Mock<IRepository<UserMorningChecklistEntity>>();
            var service = new UserMorningChecklistService(repositoryMock.Object);

            var userMorningChecklistEntity = new UserMorningChecklistEntity { UserId = userId, MorningChecklistItemId = morningChecklistItemId };
            var userMorningChecklistEntities = new[] { userMorningChecklistEntity }.AsQueryable();
            repositoryMock.Setup(repo => repo.Get(It.IsAny<Expression<System.Func<UserMorningChecklistEntity, bool>>>()))
                .Returns((Expression<System.Func<UserMorningChecklistEntity, bool>> predicate) => userMorningChecklistEntities.Where(predicate).ToList());

            // Act
            service.CompleteUserMorningChecklistItem(userId, morningChecklistItemId);

            // Assert
            repositoryMock.Verify(repo => repo.Update(It.Is<UserMorningChecklistEntity>(entity =>
                entity.UserId == userId &&
                entity.MorningChecklistItemId == morningChecklistItemId &&
                entity.IsDone == true &&
                entity.CreatedAt.Date == DateTime.UtcNow.Date
            )), Times.Once);
        }

        [Fact]
        public void CompleteUserMorningChecklistItem_ItemNotFound_ThrowsException()
        {
            // Arrange
            int userId = 1;
            int morningChecklistItemId = 123;
            var repositoryMock = new Mock<IRepository<UserMorningChecklistEntity>>();
            var service = new UserMorningChecklistService(repositoryMock.Object);

            repositoryMock.Setup(repo => repo.Get(It.IsAny<Expression<Func<UserMorningChecklistEntity, bool>>>()))
                .Returns((Expression<Func<UserMorningChecklistEntity, bool>> predicate) => Enumerable.Empty<UserMorningChecklistEntity>());

            // Act and Assert
            Assert.Throws<Exception>(() => service.CompleteUserMorningChecklistItem(userId, morningChecklistItemId));
        }
    }
}
