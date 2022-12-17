using AutoMapper;
using Moq;
using Yu_Gi_Oh_website.Models.Forum.Models;
using Yu_Gi_Oh_website.Services.Contracts;
using Yu_Gi_Oh_website.Services.Forum.Contracts;
using Yu_Gi_Oh_website.Services.Forum.Implementations;
using Yu_Gi_Oh_website.Tests.Helpers;

namespace Yu_Gi_Oh_website.Tests.ForumTests.Services
{
    public class SubCattegoryTests
    {
        private readonly IMapper mapper;
        private readonly Mock<IVisitorCountService> visitorCountServiceMock;
        private readonly Mock<ICattegoryService> cattegoryServiceMock;
        private readonly Mock<ISoftDeleteService<SubCattegory>> softDeleteServiceMock;

        private readonly Cattegory testCattegory = new Cattegory()
        {
            Id = 1,
            Name = "Test Cattegory",
            ImageUrl = "awdawd"
        };

        private readonly SubCattegory subCattegory = new SubCattegory()
        {
            Id = 1,
            Description = "adawd",
            Name = "testSubCattegory",
            Slug = "slug",
            CattegoryId = 1
        };

        public SubCattegoryTests()
        {
            mapper = MapperHelper.Mapper;
            visitorCountServiceMock = new Mock<IVisitorCountService>();
            cattegoryServiceMock = new Mock<ICattegoryService>();
            softDeleteServiceMock = new Mock<ISoftDeleteService<SubCattegory>>();

        }

        [Fact]

        public async Task AddSubCattegory_Success()
        {
            //Arrange
            var context = ContextCreationHelper.CreateContextAndAddCattegory();
            var service = new SubCattegoryService(context, mapper, visitorCountServiceMock.Object, cattegoryServiceMock.Object, softDeleteServiceMock.Object);

            // Act

            var result = await service.AddSubCattegoryAsync(subCattegory.Name, subCattegory.Description, testCattegory.Id);

            // Assert

            Assert.True(result);
            Assert.Equal(1, context.SubCattegories.Count());
            Assert.Equal(subCattegory.Name, context.SubCattegories.FirstOrDefault()!.Name);
            Assert.Equal(testCattegory.Id, context.SubCattegories.FirstOrDefault()!.CattegoryId);
        }

        [Theory, MemberData(nameof(SubCattegoryData))]
        public async Task AddSubCattegory_Failure(int cattegoryId)
        {
            //Arrange
            var context = ContextCreationHelper.CreateContext();

            var service = new SubCattegoryService(context, mapper, visitorCountServiceMock.Object, cattegoryServiceMock.Object, softDeleteServiceMock.Object);

            // Act

            var result = await service.AddSubCattegoryAsync(subCattegory.Name, subCattegory.Description, cattegoryId);

            // Assert

            Assert.False(result);
        }

        [Fact]

        public async Task EditSubCattegory_Success()
        {
            //Arrange
            var context = ContextCreationHelper.CreateContextAndAddCattegory();
            var service = new SubCattegoryService(context, mapper, visitorCountServiceMock.Object, cattegoryServiceMock.Object, softDeleteServiceMock.Object);

            var newName = "edited";
            var newDesc = "describe";
            await service.AddSubCattegoryAsync(subCattegory.Name, subCattegory.Description, testCattegory.Id);

            // Act

            var result = await service.EditSubCattegoryAsync(testCattegory.Id, newName, newDesc, testCattegory.Id, false);

            // Assert

            Assert.True(result);
            Assert.Equal(1, context.SubCattegories.Count());
            Assert.Equal(newName, context.SubCattegories.FirstOrDefault()!.Name);
            Assert.Equal(newName, context.SubCattegories.FirstOrDefault()!.Name);
            Assert.False(context.SubCattegories.FirstOrDefault()!.IsDeleted);
            Assert.Equal(testCattegory.Id, context.SubCattegories.FirstOrDefault()!.CattegoryId);
        }

        [Fact]

        public async Task EditSubCattegory_Failure()
        {
            //Arrange
            var context = ContextCreationHelper.CreateContextAndAddCattegory();
            var service = new SubCattegoryService(context, mapper, visitorCountServiceMock.Object, cattegoryServiceMock.Object, softDeleteServiceMock.Object);

            var newName = "edited";
            var newDesc = "describe";
            await service.AddSubCattegoryAsync(subCattegory.Name, subCattegory.Description, testCattegory.Id);

            // Act

            var result = await service.EditSubCattegoryAsync(testCattegory.Id + 1, newName, newDesc, testCattegory.Id, false);

            // Assert

            Assert.False(result);
        }

        [Fact]

        public async Task GetByIfAsync_Success()
        {
            //Arrange
            var context = ContextCreationHelper.CreateContext().AddCattegorySubCattegoryAndThread();
            visitorCountServiceMock.Setup(x => x.GetTotalCountByThreadIdsAsync(It.IsAny<IEnumerable<int>>())).ReturnsAsync(new Dictionary<int, int>());
            var service = new SubCattegoryService(context, mapper, visitorCountServiceMock.Object, cattegoryServiceMock.Object, softDeleteServiceMock.Object);

            // Act

            var result = await service.GetByIdAsync(1, 1, 2);

            // Assert

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("adawd", result.Description);
            Assert.Null(result.ImageUrl);

        }

        [Fact]

        public async Task GetSubCattegoryAdminDetailsAsync_Success()
        {
            //Arrange
            var context = ContextCreationHelper.CreateContext().AddCattegorySubCattegoryAndThread();

            var service = new SubCattegoryService(context, mapper, visitorCountServiceMock.Object, cattegoryServiceMock.Object, softDeleteServiceMock.Object);

            // Act

            var result = await service.GetSubCattegoryAdminDetailsAsync();

            // Assert

            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(1, result.First().Id);
            Assert.Equal("testSubCattegory", result.First().Name);
        }




        public static IEnumerable<object[]> SubCattegoryData => new List<object[]>
        {
            new object[]{-1},
            new object[]{0},
            new object[]{4},
        };


    }
}
