using Yu_Gi_Oh_website.Models.Forum.Models;

namespace Yu_Gi_Oh_website.Services.Forum.Contracts
{
    public interface IVotesService
    {      
        
        Task<int> PostVote(int postId, string userId, bool isUpvote);

        Task<PostVote?> GetVoteByUserIdPostId(string userId, int postId);
    }
}