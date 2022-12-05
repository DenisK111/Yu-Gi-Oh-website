using Microsoft.EntityFrameworkCore;
using Yu_Gi_Oh_website.Models.Forum.Models;
using Yu_Gi_Oh_website.Services.Contracts;
using Yu_Gi_Oh_website.Services.Forum.Contracts;
using Yu_Gi_Oh_website.Web.Data;

namespace Yu_Gi_Oh_website.Services.Forum.Implementations
{
    public class VotesService : IVotesService
    {
        private readonly ApplicationDbContext context;
        private readonly ISoftDeleteService<PostVote> postVoteDeleteService;

        public VotesService(ISoftDeleteService<PostVote> postDeleteService, ApplicationDbContext context)
        {
            this.context = context;
            this.postVoteDeleteService = postDeleteService;


        }

        /// <summary>
        /// Returns Value Based on the type of vote:
        /// 1 - Adding new Vote
        /// 2 - Negative/Positive Vote Changing to Positive / Negative
        /// -1 - Removing a vote
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="userId"></param>
        /// <param name="isUpvote"></param>
        /// <returns>int</returns>
        public async Task<int> PostVote(int postId, string userId, bool isUpvote)
        {
            var userPost = await context.PostVotes.IgnoreQueryFilters<PostVote>()
                .Where(x => x.PostId == postId && x.UserId == userId)
                .FirstOrDefaultAsync();

            var returnValue = 0;

            if (userPost == null)
            {
                await context.AddAsync(new PostVote()
                {
                    UserId = userId,
                    IsUpvote = isUpvote,
                    PostId = postId,

                });
                returnValue = 1;

            }

            else if (userPost.IsUpvote != isUpvote && userPost.IsDeleted)
            {
                userPost.IsUpvote = isUpvote;
                postVoteDeleteService.Undelete(userPost);
                returnValue = 1;
            }

            else if (userPost.IsUpvote != isUpvote)
            {
                userPost.IsUpvote = isUpvote;
                returnValue = 2;
            }

            else if (userPost.IsDeleted)
            {
                postVoteDeleteService.Undelete(userPost);
                returnValue = 1;
            }

            else
            {
                postVoteDeleteService.SoftDelete(userPost);
                returnValue = -1;
            }

            await context.SaveChangesAsync();
            return returnValue;


        }    

        public async Task<PostVote?> GetVoteByUserIdPostId(string userId, int postId)
        {
            return await context.PostVotes.FirstOrDefaultAsync(x => x.UserId == userId && x.PostId == postId);
        }

    }
}
