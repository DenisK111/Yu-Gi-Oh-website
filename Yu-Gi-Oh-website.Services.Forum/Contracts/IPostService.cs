using Yu_Gi_Oh_website.Services.Forum.Models;

namespace Yu_Gi_Oh_website.Services.Forum.Contracts
{
    public interface IPostService
    {
        Task<ThreadInfoDto> AddPost(int threadId, string postContent, string authorName);

        Task<ThreadInfoDto?> RemovePost(int postId);
        
    }
}
