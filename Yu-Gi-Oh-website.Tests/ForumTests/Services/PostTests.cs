using Microsoft.AspNetCore.Identity;
using Moq;
using Yu_Gi_Oh_website.Models;
using Yu_Gi_Oh_website.Models.Forum.Models;
using Yu_Gi_Oh_website.Services.Contracts;
using Yu_Gi_Oh_website.Services.Forum.Contracts;
using Yu_Gi_Oh_website.Services.Forum.Implementations;
using Yu_Gi_Oh_website.Tests.Helpers;
using Yu_Gi_Oh_website.Tests.Moqs;

namespace Yu_Gi_Oh_website.Tests.ForumTests.Services
{
    public class PostTests
    {
        private readonly UserManager<ApplicationUser> moqUserManager;
        private readonly Mock<IEntityByIdService> entityServiceMock;
        private readonly Mock<ISoftDeleteService<Post>> softDeleteServiceMock;

        public PostTests()
        {
            moqUserManager = new MoqUserManager();
            this.entityServiceMock = new Mock<IEntityByIdService>();
            this.softDeleteServiceMock = new Mock<ISoftDeleteService<Post>>();
        }
        [Fact]
        public async Task AddPost_Success()
        {   //Arrange
            var context = ContextCreationHelper.CreateContext().AddCattegorySubCattegoryAndThread();
            entityServiceMock.Setup(x => x.GetThreadById(It.IsAny<int>())).ReturnsAsync(TestConstants.Forum.Thread);
            entityServiceMock.Setup(x => x.GetAuthorByUserName(It.IsAny<string>())).ReturnsAsync(TestConstants.Forum.User);
            var service = new PostService(context, entityServiceMock.Object, moqUserManager, softDeleteServiceMock.Object);
            // Act
            var result = await service.AddPost(TestConstants.Forum.ThreadId, TestConstants.Forum.PostContentContent, "any");

            //Assert

            Assert.NotNull(result);
            Assert.Equal(TestConstants.Forum.ThreadId, result.Id);
            Assert.Equal(TestConstants.Forum.SubCattegoryId, result.SubCattegoryId);
            Assert.Equal(TestConstants.Forum.Slug, result.SubCattegorySlug);
            Assert.Equal(1, result.CurrentPage);
        }

        [Fact]
        public async Task AddPost_ThreadDoesNotExist_Failure()
        {
            //Arrange
            var context = ContextCreationHelper.CreateContext().AddCattegorySubCattegoryAndThread();
            entityServiceMock.Setup(x => x.GetThreadById(It.IsAny<int>())).ReturnsAsync((ForumThread?)null);

            var service = new PostService(context, entityServiceMock.Object, moqUserManager, softDeleteServiceMock.Object);
            // Act
            var result = await service.AddPost(TestConstants.Forum.ThreadId, TestConstants.Forum.PostContentContent, "any");

            //Assert

            Assert.NotNull(result);
            Assert.True(result.IsError);
            Assert.Equal("Post must belong to an existing Thread.", result!.ErrorMessage);

        }


        [Fact]
        public async Task AddPost_AuthorDoesNotExist_Failure()
        {
            //Arrange
            var context = ContextCreationHelper.CreateContext().AddCattegorySubCattegoryAndThread();
            entityServiceMock.Setup(x => x.GetThreadById(It.IsAny<int>())).ReturnsAsync(TestConstants.Forum.Thread);
            entityServiceMock.Setup(x => x.GetAuthorByUserName(It.IsAny<string>())).ReturnsAsync((ApplicationUser?)null);

            var service = new PostService(context, entityServiceMock.Object, moqUserManager, softDeleteServiceMock.Object);
            // Act
            var result = await service.AddPost(TestConstants.Forum.ThreadId, TestConstants.Forum.PostContentContent, "any");

            //Assert

            Assert.NotNull(result);
            Assert.True(result.IsError);
            Assert.Equal("Post must have an Author.", result!.ErrorMessage);

        }
        [Fact]
        public async Task Remove_Success()
        {   //Arrange
            var context = ContextCreationHelper.CreateContext().AddCattegorySubCattegoryAndThread();
            context.Posts.Add(new Post()
            {
                Author = context.Users.Find(TestConstants.Forum.UserId)!,
                Thread = context.Threads.Find(TestConstants.Forum.ThreadId)!,
                Id = TestConstants.Forum.PostId,
                Votes = new List<PostVote>(),
                PostContent = context.PostContents.Find(TestConstants.Forum.PostContentId)!,
            });
            context.SaveChanges();

            var service = new PostService(context, entityServiceMock.Object, moqUserManager, softDeleteServiceMock.Object);

            // Act
            var result = await service.RemovePost(TestConstants.Forum.PostId);
            //Assert

            Assert.NotNull(result);
            Assert.Equal(TestConstants.Forum.ThreadId, result!.Id);
            Assert.Equal(TestConstants.Forum.SubCattegoryId, result.SubCattegoryId);
            Assert.Equal(TestConstants.Forum.Slug, result.SubCattegorySlug);
            Assert.Equal(1, result.CurrentPage);

        }

        [Fact]
        public async Task Remove_IdDoesNotExist_Failure()
        {
            //Arrange
            var context = ContextCreationHelper.CreateContext().AddCattegorySubCattegoryAndThread();
            var service = new PostService(context, entityServiceMock.Object, moqUserManager, softDeleteServiceMock.Object);

            // Act

            var result = await service.RemovePost(TestConstants.Forum.PostId);

            //Assert

            Assert.Null(result);
        }
    }
}
