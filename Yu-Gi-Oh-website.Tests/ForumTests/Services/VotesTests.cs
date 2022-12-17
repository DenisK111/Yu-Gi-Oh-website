using Microsoft.EntityFrameworkCore;
using Moq;
using Yu_Gi_Oh_website.Models.Forum.Models;
using Yu_Gi_Oh_website.Services.Contracts;
using Yu_Gi_Oh_website.Services.Forum.Implementations;
using Yu_Gi_Oh_website.Tests.Helpers;

namespace Yu_Gi_Oh_website.Tests.ForumTests.Services
{
    public class VotesTests
    {
        private readonly Mock<ISoftDeleteService<PostVote>> postVoteDeleteServiceMock;

        public VotesTests()
        {
            postVoteDeleteServiceMock = new Mock<ISoftDeleteService<PostVote>>();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task PostVote_AddNewVote_Success(bool isUpvote)
        {   //Arrange
            var context = ContextCreationHelper.CreateContext().AddCattegory().AddSubCattegory().AddThread().AddPost();

            var service = new VotesService(postVoteDeleteServiceMock.Object, context);
            // Act
            var resultUpVote = await service.PostVote(TestConstants.Forum.PostId, TestConstants.Forum.UserId, isUpvote);

            //Assert

            Assert.Equal(1, resultUpVote);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task PostVote_AddChangeVote_WasDeleted_Success(bool isUpvote)
        {   
            //Arrange
            var context = ContextCreationHelper.CreateContext().AddCattegory().AddSubCattegory().AddThread().AddPost()
                .AddDeletedPostVote(!isUpvote);

            postVoteDeleteServiceMock.Setup(x => x.Undelete(It.IsAny<PostVote>()))
                .Callback(() =>
            {
                context.PostVotes.IgnoreQueryFilters().Single().IsUpvote = isUpvote;
                context.PostVotes.IgnoreQueryFilters().Single().IsDeleted = false;
                return;
            });

            var service = new VotesService(postVoteDeleteServiceMock.Object, context);
            // Act
            var resultUpVote = await service.PostVote(TestConstants.Forum.PostId, TestConstants.Forum.UserId, isUpvote);

            //Assert

            Assert.Equal(1, resultUpVote);
            Assert.Equal(isUpvote, context.PostVotes.Single().IsUpvote);
        }


        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task PostVote_AddVote_WasDeleted_Success(bool isUpvote)
        {
            //Arrange
            var context = ContextCreationHelper.CreateContext().AddCattegory().AddSubCattegory().AddThread().AddPost()
                .AddDeletedPostVote(isUpvote);

            postVoteDeleteServiceMock.Setup(x => x.Undelete(It.IsAny<PostVote>()))
                .Callback(() =>
                {
                    context.PostVotes.IgnoreQueryFilters().Single().IsDeleted = false;
                    return;
                });

            var service = new VotesService(postVoteDeleteServiceMock.Object, context);
            // Act
            var resultUpVote = await service.PostVote(TestConstants.Forum.PostId, TestConstants.Forum.UserId, isUpvote);

            //Assert

            Assert.Equal(1, resultUpVote);
            Assert.Equal(isUpvote, context.PostVotes.Single().IsUpvote);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task PostVote_ChangeVote_Success(bool isUpvote)
        {   //Arrange
            var context = ContextCreationHelper.CreateContext().AddCattegory().AddSubCattegory().AddThread().AddPost().AddPostVote(!isUpvote);

            var service = new VotesService(postVoteDeleteServiceMock.Object, context);
            // Act
            var resultUpVote = await service.PostVote(TestConstants.Forum.PostId, TestConstants.Forum.UserId, isUpvote);

            //Assert

            Assert.Equal(2, resultUpVote);
            Assert.Equal(isUpvote, context.PostVotes.Single().IsUpvote);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task PostVote_RemoveVote_Success(bool isUpvote)
        {
            //Arrange
            var context = ContextCreationHelper.CreateContext().AddCattegory().AddSubCattegory().AddThread().AddPost()
                .AddPostVote(isUpvote);

            postVoteDeleteServiceMock.Setup(x => x.SoftDelete(It.IsAny<PostVote>()))
                .Callback(() =>
                {
                    context.PostVotes.Single().IsDeleted = true;
                    return;
                });

            var service = new VotesService(postVoteDeleteServiceMock.Object, context);
            // Act
            var resultUpVote = await service.PostVote(TestConstants.Forum.PostId, TestConstants.Forum.UserId, isUpvote);

            //Assert

            Assert.Equal(-1, resultUpVote);
            Assert.Null(context.PostVotes.SingleOrDefault());
            Assert.Equal(isUpvote, context.PostVotes.IgnoreQueryFilters().Single().IsUpvote);
        }
    }
}
