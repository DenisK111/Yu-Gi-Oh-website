using Microsoft.AspNetCore.Identity;
using Yu_Gi_Oh_website.Models;
using Yu_Gi_Oh_website.Services.Forum.Implementations;
using Yu_Gi_Oh_website.Tests.Helpers;
using Yu_Gi_Oh_website.Tests.Moqs;

namespace Yu_Gi_Oh_website.Tests.ForumTests.Services
{
    public class EntityByIdTests
    {
        private readonly UserManager<ApplicationUser> moqUserManager;

        public EntityByIdTests()
        {            
            moqUserManager = new MoqUserManager();
        }
        [Fact]
        public async Task GetThreadById_Success()
        {   //Arrange
            var context = ContextCreationHelper.CreateContext().AddCattegorySubCattegoryAndThread();
            var service = new EntityByIdService(context, moqUserManager);
            // Act
            var result = await service.GetThreadById(TestConstants.Forum.ThreadId);

            //Assert

            Assert.NotNull(result);
            Assert.Equal(TestConstants.Forum.ThreadId, result!.Id);
            
        }

        [Fact]
        public async Task GetThreadByIdNull_Success()
        {   //Arrange
            var context = ContextCreationHelper.CreateContext().AddCattegorySubCattegoryAndThread();
            var service = new EntityByIdService(context, moqUserManager);
            // Act
            var result = await service.GetThreadById(TestConstants.Forum.ThreadId+1);

            //Assert

            Assert.Null(result);            

        }
        [Fact]
        public async Task GetAuthorByUserName_Success()
        {
            //Arrange
            var context = ContextCreationHelper.CreateContext();            
            var service = new EntityByIdService(context, moqUserManager);

            // Act

            var result = await service.GetAuthorByUserName("d");

            // Assert

            Assert.NotNull(result);                   
            Assert.Equal(TestConstants.Forum.UserId,result!.Id);                   
        }
    }
}
