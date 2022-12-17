using Moq;
using Yu_Gi_Oh_website.Data.Repositories.Contracts;
using Yu_Gi_Oh_website.Services.Forum.Implementations;

namespace Yu_Gi_Oh_website.Tests.ForumTests.Services
{
    public class VisitorCountTests
    {
        private readonly Mock<IVisitorCountRepository> visitorCountRepositoryMock;

        public VisitorCountTests()
        {
            this.visitorCountRepositoryMock = new Mock<IVisitorCountRepository>();
        }

        [Fact]

        public void AddOrUpdateAsync_Success()
        {
            //Arrange

            var service = new VisitorCountService(visitorCountRepositoryMock.Object);

            // Act

            Task addOrUpdate = service.AddOrUpdateAsync("awdaw", "awdawd", TestConstants.Forum.ThreadId);
            addOrUpdate.Wait();

            //Assert

            Assert.True(addOrUpdate.IsCompletedSuccessfully);                        
        }

        [Fact]

        public async Task GetTotalCountByThreadIdsAsync_Success()
        {
            //Arrange

            visitorCountRepositoryMock.Setup(x => x.GetTotalCountByThreadIdsAsync(It.IsAny<IEnumerable<int>>())).ReturnsAsync(new Dictionary<int, int>() { [1] = 1, [2] = 2 });
            var service = new VisitorCountService(visitorCountRepositoryMock.Object);

            // Act

            var result = await service.GetTotalCountByThreadIdsAsync(Enumerable.Empty<int>());

            //Assert

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal(1, result[1]);
            Assert.Equal(2, result[2]);
        }
    }
}
