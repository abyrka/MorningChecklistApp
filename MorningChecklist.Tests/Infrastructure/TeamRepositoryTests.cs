using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using MorningChecklist.Infrastructure.Entities;
using MorningChecklist.Infrastructure.Repositories;
using MorningChecklist.Infrastructure;
using Moq;

namespace MorningChecklist.Tests.Infrastructure
{
    public class TeamRepositoryTests
    {
        [Fact]
        public void GetById_ValidId_ReturnsEntity()
        {
            // Arrange
            int entityId = 1;
            var dbContextMock = CreateDbContextMock();
            var repository = new Repository<TeamEntity>(dbContextMock.Object);

            // Act
            var result = repository.GetById(entityId);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(entityId);
        }

        [Fact]
        public void GetAll_ReturnsAllEntities()
        {
            // Arrange
            var dbContextMock = CreateDbContextMock();
            var repository = new Repository<TeamEntity>(dbContextMock.Object);

            // Act
            var result = repository.GetAll();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
        }

        [Fact]
        public void Add_ValidEntity_AddsToContext()
        {
            // Arrange
            var dbContextMock = CreateDbContextMock();
            var repository = new Repository<TeamEntity>(dbContextMock.Object);

            var newEntity = new TeamEntity { Id = 3, Name = "New Team 3" };

            // Act
            repository.Add(newEntity);

            // Assert
            dbContextMock.Verify(context => context.SaveChanges(), Times.Once);
        }

        // Add more test cases for other methods

        private Mock<ApplicationDbContext> CreateDbContextMock()
        {
            var dbContextMock = new Mock<ApplicationDbContext>();

            // Mock DbSet
            var data = new List<TeamEntity>
            {
                new TeamEntity { Id = 1, Name = "Team 1" },
                new TeamEntity { Id = 2, Name = "Team 2" },
            }.AsQueryable();

            var dbSetMock = new Mock<DbSet<TeamEntity>>();
            dbSetMock.As<IQueryable<TeamEntity>>().Setup(m => m.Provider).Returns(data.Provider);
            dbSetMock.As<IQueryable<TeamEntity>>().Setup(m => m.Expression).Returns(data.Expression);
            dbSetMock.As<IQueryable<TeamEntity>>().Setup(m => m.ElementType).Returns(data.ElementType);
            dbSetMock.As<IQueryable<TeamEntity>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            dbContextMock.Setup(context => context.Set<TeamEntity>()).Returns(dbSetMock.Object);

            return dbContextMock;
        }
    }
}
