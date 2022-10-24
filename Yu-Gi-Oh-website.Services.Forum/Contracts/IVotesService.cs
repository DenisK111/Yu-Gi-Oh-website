namespace Yu_Gi_Oh_website.Services.Forum.Contracts
{
    public interface IVotesService
    {
        Task<int> AddThreadVote(int threadId, string userId, bool isUpvote);
        //Task<(int upVotes, int DownVotes)> GetAllPostVotes(int postId);
        Task PostVote(int postId, string userId, bool isUpvote);
    }
}