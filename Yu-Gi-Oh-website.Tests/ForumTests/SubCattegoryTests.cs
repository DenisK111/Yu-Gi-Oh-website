using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using Yu_Gi_Oh_website.Models.Forum.Models;
using Yu_Gi_Oh_website.Services.Contracts;
using Yu_Gi_Oh_website.Services.Forum.Contracts;
using Yu_Gi_Oh_website.Services.Forum.Implementations;
using Yu_Gi_Oh_website.Tests.Helpers;
using Yu_Gi_Oh_website.Web.Data;

namespace Yu_Gi_Oh_website.Tests.ForumTests
{
    public class SubCattegoryTests
    {
        private readonly IMapper mapper;
        private readonly Mock<IVisitorCountService> visitorCountService;
        private readonly Mock<ICattegoryService> cattegoryService;
        private readonly Mock<ISoftDeleteService<SubCattegory>> softDeleteService;
        private readonly ApplicationDbContext context;

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
            this.mapper = MapperHelper.Mapper;
            this.visitorCountService = new Mock<IVisitorCountService>();
            this.cattegoryService = new Mock<ICattegoryService>();
            this.softDeleteService = new Mock<ISoftDeleteService<SubCattegory>>();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                            .UseInMemoryDatabase(databaseName: "SubCattegory")
                            .Options;

            this.context = new ApplicationDbContext(options);

        }

        [Fact]

        public async Task AddSubCattegory_Success()
        {
            //Arrange


            this.context.Cattegories.Add(testCattegory);
            var service = new SubCattegoryService(context, mapper, visitorCountService.Object, cattegoryService.Object, softDeleteService.Object);

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


            this.context.Cattegories.Add(testCattegory);
            var service = new SubCattegoryService(context, mapper, visitorCountService.Object, cattegoryService.Object, softDeleteService.Object);

            // Act

            var result = await service.AddSubCattegoryAsync(subCattegory.Name, subCattegory.Description, cattegoryId);

            // Assert

            Assert.False(result);          
        }



        public static IEnumerable<object[]> SubCattegoryData => new List<object[]>
        {
            new object[]{-1},
            new object[]{0},
            new object[]{4},
        };
    }
}
