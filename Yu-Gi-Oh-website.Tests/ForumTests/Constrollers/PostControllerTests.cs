using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NToastNotify;
using Yu_Gi_Oh_website.Services.Forum.Contracts;
using Yu_Gi_Oh_website.Services.Forum.Models;
using Yu_Gi_Oh_website.Tests.Helpers;
using Yu_Gi_Oh_website.Web.Areas.Forum.Controllers;
using Yu_Gi_Oh_website.Web.Areas.Forum.Models;

namespace Yu_Gi_Oh_website.Tests.ForumTests.Constrollers
{
    public class PostControllerTests
    {
        private readonly Mock<IPostService> postServiceMock;
        private readonly Mock<IThreadService> threadServiceMock;
        private readonly Mock<ISubCattegoryService> subCattegoryServiceMock;
        private readonly Mock<IToastNotification> toastNotificationMock;
        private readonly Mock<IHttpContextAccessor> httpContextMock;
        private readonly IMapper mapper;

        public PostControllerTests()
        {
            postServiceMock = new Mock<IPostService>();
            threadServiceMock = new Mock<IThreadService>();
            subCattegoryServiceMock = new Mock<ISubCattegoryService>();
            toastNotificationMock = new Mock<IToastNotification>();
            httpContextMock = new Mock<IHttpContextAccessor>();
            mapper=MapperHelper.Mapper;
        }

        [Fact]

        public async Task GetAddPost_Success()
        {
            //Arrange


            subCattegoryServiceMock.Setup(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new FullSubCattegoryDto());
            threadServiceMock.Setup(x => x.GetThreadDtoById(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync((new ThreadDto(), 0));
            var controller = new PostController(postServiceMock.Object,
                mapper,
                toastNotificationMock.Object,
                threadServiceMock.Object,
                subCattegoryServiceMock.Object,
                httpContextMock.Object);


            // Act
            var result = await controller.AddPost(1,1);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<AddPostInputViewModel>(
                viewResult.ViewData.Model);
            Assert.Equal(1, model.SubCattegoryId);
            Assert.Equal(1, model.ThreadId);


        }

        [Fact]
        public async Task GetAddPost_ThreadNotFound_Failure()
        {
            //Arrange


            subCattegoryServiceMock.Setup(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new FullSubCattegoryDto());
            threadServiceMock.Setup(x => x.GetThreadDtoById(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync((null!, 0));
            var controller = new PostController(postServiceMock.Object,
                mapper,
                toastNotificationMock.Object,
                threadServiceMock.Object,
                subCattegoryServiceMock.Object,
                httpContextMock.Object);


            // Act
            var result = await controller.AddPost(1, 1);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("error404",viewResult.ViewName);
            Assert.Null(viewResult.Model);       
        }

        [Fact]
        public async Task GetAddPost_SubCattegoryNotFound_Failure()
        {
            //Arrange

            subCattegoryServiceMock.Setup(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync((FullSubCattegoryDto?)null!);
            threadServiceMock.Setup(x => x.GetThreadDtoById(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync((new ThreadDto(), 0));
            var controller = GenerateController();

            // Act

            var result = await controller.AddPost(1, 1);

            //Assert

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("error404", viewResult.ViewName);
            Assert.Null(viewResult.Model);
        }

        [Fact]
        public async Task PostAddPost_Success()
        {
            //Arrange

            var post = new AddPostInputViewModel()
            {
                PostContent = "awdawdawdawd",
                Author="adawdadw",
                ThreadId = 2,
                SubCattegoryId = 1,
                SubCattegorySlug="awdawd"
            };

            postServiceMock.Setup(x => x.AddPost(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new ThreadInfoDto());

            var controller = GenerateController();

            // Act

            var result = await controller.AddPost(post);

            //Assert

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Thread", redirectResult.ControllerName);
            Assert.Equal("Thread",redirectResult.ActionName);
        }

        [Fact]
        public async Task PostAddPost_ModelStateInvalid_Failure()
        {
            //Arrange

            var post = new AddPostInputViewModel()
            {
                PostContent = "s",
                Author = "adawdadw",
                ThreadId = 2,
                SubCattegoryId = 1,
                SubCattegorySlug = "awdawd"
            };            

            var controller = GenerateController();
            controller.ModelState.AddModelError("SessionName", "Required");

            // Act

            var result = await controller.AddPost(post);

            //Assert

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<AddPostInputViewModel>(
                viewResult.ViewData.Model);
            Assert.Equal(post.PostContent,model.PostContent);
            Assert.Equal(post.ThreadId,model.ThreadId);
            Assert.Equal(post.Author, model.Author);
            Assert.Equal(post.SubCattegorySlug, model.SubCattegorySlug);
            Assert.Equal(post.SubCattegoryId, model.SubCattegoryId);
        }

        [Fact]
        public async Task PostAddPost_InvalidThreadIdOrAuthor_Failure()
        {
            //Arrange

            var post = new AddPostInputViewModel()
            {
                PostContent = "s",
                Author = "adawdadw",
                ThreadId = 2,
                SubCattegoryId = 1,
                SubCattegorySlug = "awdawd"
            };

            postServiceMock.Setup(x => x.AddPost(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>()))
               .ReturnsAsync(new ThreadInfoDto() { IsError=true,ErrorMessage="Error"});

            var controller = GenerateController();          

            // Act

            var result = await controller.AddPost(post);

            //Assert

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<AddPostInputViewModel>(
                viewResult.ViewData.Model);
            Assert.Equal(post.PostContent, model.PostContent);
            Assert.Equal(post.ThreadId, model.ThreadId);
            Assert.Equal(post.Author, model.Author);
            Assert.Equal(post.SubCattegorySlug, model.SubCattegorySlug);
            Assert.Equal(post.SubCattegoryId, model.SubCattegoryId);
            Assert.Equal(1, viewResult.ViewData.ModelState.ErrorCount);
            Assert.Equal("Error", viewResult.ViewData.ModelState.Values.SelectMany(v => v.Errors).ToList()[0].ErrorMessage);
        }

        [Fact]
        public async Task RemovePost_Success() 
        {
            //Arrange           

            postServiceMock.Setup(x => x.RemovePost(It.IsAny<int>()))
               .ReturnsAsync(new ThreadInfoDto());

            var controller = GenerateController();

            // Act

            var result = await controller.RemovePost(1,1);

            //Assert

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Thread", redirectResult.ControllerName);
            Assert.Equal("Thread", redirectResult.ActionName);
        }

        private PostController GenerateController()
        {

            return new PostController(postServiceMock.Object,
                mapper,
                toastNotificationMock.Object,
                threadServiceMock.Object,
                subCattegoryServiceMock.Object,
                httpContextMock.Object);
        }
    }
}
