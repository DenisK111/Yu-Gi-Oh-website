using Microsoft.EntityFrameworkCore;
using Yu_Gi_Oh_website.Models.Forum.Models;
using Yu_Gi_Oh_website.Services.Contracts;
using Yu_Gi_Oh_website.Services.Forum.Contracts;
using Yu_Gi_Oh_website.Web.Data;

namespace Yu_Gi_Oh_website.Services.Forum.Implementations
{
    public class VotesService : IVotesService
    {
        private readonly ISoftDeleteService<PostVote> postDeleteService;
        private readonly ISoftDeleteService<ThreadVote> threadDeleteService;
        private readonly ApplicationDbContext context;

        public VotesService(ISoftDeleteService<PostVote> postDeleteService, ISoftDeleteService<ThreadVote> threadDeleteService, ApplicationDbContext context)
        {
            this.postDeleteService = postDeleteService;
            this.threadDeleteService = threadDeleteService;
            this.context = context;

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
                postDeleteService.Undelete(userPost);
                returnValue = 1;
            }

            else if(userPost.IsUpvote != isUpvote)
            {
                userPost.IsUpvote= isUpvote;
                returnValue = 2;
            }

            else if (userPost.IsDeleted)
            {
                postDeleteService.Undelete(userPost);
                returnValue = 1;
            }

            else
            {
                postDeleteService.SoftDelete(userPost);
                returnValue = -1;
            }

            await context.SaveChangesAsync();
            return returnValue;


        }

        /// <summary>
        /// Returns Value Based on the type of vote:
        /// 1 - Adding new Vote
        /// 2 - Negative/Positive Vote Changing to Positive / Negative
        /// -1 - Removing a vote
        /// </summary>
        /// <param name="threadId"></param>
        /// <param name="userId"></param>
        /// <param name="isUpvote"></param>
        /// <returns></returns>

        public async Task<int> AddThreadVote(int threadId, string userId, bool isUpvote)
        {
            var userThread = await context.ThreadVotes.IgnoreQueryFilters<ThreadVote>()
                .Where(x => x.ThreadId == threadId && x.UserId == userId)
                .FirstOrDefaultAsync();

            var returnValue = 0;

            if (userThread == null)
            {
                await context.AddAsync(new ThreadVote()
                {
                    UserId = userId,
                    IsUpvote = isUpvote,
                    ThreadId = threadId,

                });


                returnValue = 1;
            }

            else if (userThread.IsUpvote != isUpvote)
            {
                userThread.IsUpvote = isUpvote;
                threadDeleteService.Undelete(userThread);

                returnValue = 2;
            }

            else
            {
                threadDeleteService.SoftDelete(userThread);
                returnValue = -1;
            }

            await context.SaveChangesAsync();
            return returnValue;
        }

        public async Task<PostVote?> GetVoteByUserIdPostId(string userId, int postId)
        {
            return await context.PostVotes.FirstOrDefaultAsync(x=>x.UserId==userId && x.PostId==postId);
        }

        //public async Task<(int upVotes, int DownVotes)> GetAllPostVotes(int postId)
        //{
        //    var upVotes = await context.PostVotes
        //        .Where(x => x.PostId == postId)
        //        .CountAsync(x => x.IsUpvote);
        //    var donwVotes = await context.PostVotes
        //        .Where(x => x.PostId == postId)
        //        .CountAsync(x => !x.IsUpvote);

        //    return (upVotes, donwVotes);
        //}

        //public async Task<(int upVotes, int DownVotes)> GetAllThreadVotes(int threadId)
        //{
        //    var upVotes = await context.ThreadVotes
        //        .Where(x => x.PostId == postId)
        //        .CountAsync(x => x.IsUpvote);
        //    var donwVotes = await context.ThreadVotes
        //        .Where(x => x.PostId == postId)
        //        .CountAsync(x => !x.IsUpvote);

        //    return (upVotes, donwVotes);
        //}

    }
}
