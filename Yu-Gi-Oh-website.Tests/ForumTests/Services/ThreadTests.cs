using AutoMapper;
using Moq;
using Yu_Gi_Oh_website.Models;
using Yu_Gi_Oh_website.Services.Forum.Contracts;
using Yu_Gi_Oh_website.Services.Forum.Implementations;
using Yu_Gi_Oh_website.Services.Forum.Models;
using Yu_Gi_Oh_website.Tests.Helpers;

namespace Yu_Gi_Oh_website.Tests.ForumTests.Services
{
    public class ThreadTests
    {
        private readonly Mock<IEntityByIdService> entityServiceMock;
        private readonly Mock<IPostService> postServiceMock;
        private readonly IMapper mapper;
        public ThreadTests()
        {
            mapper = MapperHelper.Mapper;
            this.entityServiceMock = new Mock<IEntityByIdService>();
            this.postServiceMock = new Mock<IPostService>();
        }

        [Fact]

        public async Task CreateThread_Success()
        {
            //Arrange
            var context = ContextCreationHelper.CreateContext().AddCattegory().AddSubCattegory();            
            entityServiceMock.Setup(x => x.GetAuthorByUserName(It.IsAny<string>()))
                .ReturnsAsync(TestConstants.Forum.User);
            postServiceMock.Setup(x => x.AddPost(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new ThreadInfoDto() { Id=TestConstants.Forum.ThreadId});
            var service = new ThreadService(context, mapper, postServiceMock.Object,entityServiceMock.Object);
            // Act
            var result = await service.CreateThread(TestConstants.Forum.ThreadSubject, TestConstants.Forum.PostContentContent, "any",TestConstants.Forum.SubCattegoryId);
            var thread = context.SubCattegories.Single(x => x.Id == TestConstants.Forum.SubCattegoryId).Threads.SingleOrDefault(x => x.Subject == TestConstants.Forum.ThreadSubject);
            //Assert

            Assert.NotNull(result);
            Assert.Equal(TestConstants.Forum.ThreadId, result.Id);
            Assert.Equal(1, context.SubCattegories.Count());
            Assert.NotNull(thread);
            Assert.Equal(TestConstants.Forum.SubCattegoryId, thread!.SubCattegoryId);          
            
        }
        [Fact]
        public async Task CreateThread_SubjectAlreadeyExists_Failure()
        {
            //Arrange
            var context = ContextCreationHelper.CreateContext().AddCattegory().AddSubCattegory().AddThread();
            
            var service = new ThreadService(context, mapper, postServiceMock.Object, entityServiceMock.Object);
            // Act
            var result = await service.CreateThread(TestConstants.Forum.ThreadSubject, TestConstants.Forum.PostContentContent, "any", TestConstants.Forum.SubCattegoryId);
            
            //Assert

            Assert.NotNull(result);
            Assert.True(result.IsError);
            Assert.Equal("A Thread with this name already exists", result.ErrorMessage);                       
        }

        [Fact]
        public async Task CreateThread_AuthorDoesNotExist_Failure()
        {
            //Arrange
            var context = ContextCreationHelper.CreateContext().AddCattegory().AddSubCattegory();
            entityServiceMock.Setup(x => x.GetAuthorByUserName(It.IsAny<string>()))
                .ReturnsAsync((ApplicationUser?)null);
            var service = new ThreadService(context, mapper, postServiceMock.Object, entityServiceMock.Object);
            // Act
            var result = await service.CreateThread(TestConstants.Forum.ThreadSubject, TestConstants.Forum.PostContentContent, "any", TestConstants.Forum.SubCattegoryId);

            //Assert

            Assert.NotNull(result);
            Assert.True(result.IsError);
            Assert.Equal("No Such User exists in the Database", result.ErrorMessage);
        }

        [Fact]
        public async Task CreateThread_SubCategoryDoesNotExist_Failure()
        {
            //Arrange
            var context = ContextCreationHelper.CreateContext().AddCattegory();
            entityServiceMock.Setup(x => x.GetAuthorByUserName(It.IsAny<string>()))
                .ReturnsAsync(TestConstants.Forum.User);
            var service = new ThreadService(context, mapper, postServiceMock.Object, entityServiceMock.Object);
            // Act
            var result = await service.CreateThread(TestConstants.Forum.ThreadSubject, TestConstants.Forum.PostContentContent, "any", TestConstants.Forum.SubCattegoryId);

            //Assert

            Assert.NotNull(result);
            Assert.True(result.IsError);
            Assert.Equal("No Such SubCattegory Exists", result.ErrorMessage);
        }

        [Fact]
        public async Task GetThreadDtoById_NoPosts_Success()
        {
            //Arrange
            var context = ContextCreationHelper.CreateContext().AddCattegory().AddSubCattegory().AddThread();
            
            var service = new ThreadService(context, mapper, postServiceMock.Object, entityServiceMock.Object);
            // Act
            var result = await service.GetThreadDtoById(TestConstants.Forum.ThreadId, 2, 0);

            //Assert
           
            Assert.Equal(0,result.postCount);
            Assert.Equal(TestConstants.Forum.ThreadId, result.thread.Id);
            Assert.Equal(TestConstants.Forum.ThreadSubject, result.thread.Subject);
        }

        [Fact]
        public async Task GetThreadDtoById_HasPosts_Success()
        {
            //Arrange

            var context = ContextCreationHelper.CreateContext().AddCattegory().AddSubCattegory().AddThread().AddPost();
            var service = new ThreadService(context, mapper, postServiceMock.Object, entityServiceMock.Object);

            // Act

            var result = await service.GetThreadDtoById(TestConstants.Forum.ThreadId, 2, 0);

            //Assert

            Assert.Equal(1, result.postCount);
            Assert.Equal(TestConstants.Forum.ThreadId, result.thread.Id);
            Assert.Equal(TestConstants.Forum.ThreadSubject, result.thread.Subject);
            Assert.Equal(TestConstants.Forum.Post.Id, result.thread.Posts.First().Id);
            Assert.Equal(TestConstants.Forum.PostContentContent, result.thread.Posts.First().PostContent);
        }
    }
}
