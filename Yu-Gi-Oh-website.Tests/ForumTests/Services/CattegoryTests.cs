using AutoMapper;
using Yu_Gi_Oh_website.Services.Forum.Implementations;
using Yu_Gi_Oh_website.Tests.Helpers;

namespace Yu_Gi_Oh_website.Tests.ForumTests.Services
{
    public class CattegoryTests
    {
        private readonly IMapper mapper;
        public CattegoryTests()
        {
            mapper = MapperHelper.Mapper;
        }

        [Fact]

        public async Task GetCattegoryIdNameAsync_Success()
        {
            //Arrange
            var context = ContextCreationHelper.CreateContextAndAddCattegory();
            var service = new CattegoryService(context, mapper);

            // Act

            var result = await service.GetCattegoryIdNameAsync();

            // Assert

            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(TestConstants.Forum.CattegoryId, result.First().Id);
            Assert.Equal(TestConstants.Forum.CattegoryName, result.First().Name);
        }

        [Fact]

        public async Task GetallCattegoriesAsync_Success()
        {
            //Arrange
            var context = ContextCreationHelper.CreateContextAndAddCattegory();
            var service = new CattegoryService(context, mapper);

            // Act

            var result = await service.GetallCattegoriesAsync();

            // Assert

            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(TestConstants.Forum.CattegoryName, result.First().Name);
            Assert.Equal(TestConstants.Forum.CattegoryImageUrl, result.First().ImageUrl);
            Assert.Empty(result.First().SubCattegories);

        }
    }
}
