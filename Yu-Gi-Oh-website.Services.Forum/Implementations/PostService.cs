using Yu_Gi_Oh_website.Models.Forum.Models;
using Yu_Gi_Oh_website.Models;
using Yu_Gi_Oh_website.Services.Forum.Models;
using Yu_Gi_Oh_website.Services.Forum.Contracts;
using Yu_Gi_Oh_website.Web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Yu_Gi_Oh_website.Services.Contracts;

namespace Yu_Gi_Oh_website.Services.Forum.Implementations
{
    public class PostService : IPostService
    {
        private readonly ApplicationDbContext context;
        private readonly IEntityByIdService entityService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ISoftDeleteService<Post> postDeleteService;

        public PostService(ApplicationDbContext context, IEntityByIdService entityService, UserManager<ApplicationUser> userManager, ISoftDeleteService<Post> postDeleteService)
        {
            this.context = context;
            this.entityService = entityService;
            this.userManager = userManager;
            this.postDeleteService = postDeleteService;
        }
        public async Task<ThreadInfoDto> AddPost(int threadId, string postContent, string authorName)
        {
            var thread = await entityService.GetThreadById(threadId);

            if (thread == null) return new ThreadInfoDto()
            {
                IsError = true,
                ErrorMessage = "Post must belong to an existing Thread."
            };

            var author = await entityService.GetAuthorByUserName(authorName);

            if (author == null) return new ThreadInfoDto()
            {
                IsError = true,
                ErrorMessage = "Post must have an Author."
            };

            return await this.AddPost(thread, postContent, author);
        }

        public async Task<ThreadInfoDto?> RemovePost(int postId)
        {
            var post = await
                context.Posts
                .Include(x => x.Thread)
                .ThenInclude(x => x.SubCattegory)
                .Include(x=>x.Author)
                .FirstOrDefaultAsync(x => x.Id == postId);

            if (post is null)
            {
                return null;
            }
            postDeleteService.SoftDelete(post);
            post.Author.PostCount--;
            await context.SaveChangesAsync();

            return new ThreadInfoDto()
            {
                Id = post.Thread.Id,
                SubCattegoryId = post.Thread.SubCattegoryId,
                SubCattegorySlug = post.Thread.SubCattegory.Slug,
                CurrentPage = 1,
            };
        }

        private async Task<ThreadInfoDto> AddPost(ForumThread thread, string content, ApplicationUser author)
        {

            var post = new Post()
            {
                Author = author,
                Thread = thread,
                PostContent = new PostContent()
                {
                    Content = content,
                },
            };

            thread.Posts.Add(post);
            thread.ModifiedOn = DateTime.UtcNow;
            author.PostCount++;
            await userManager.UpdateAsync(author);
            await context.SaveChangesAsync();

            var threadId = (await context.Threads.FirstOrDefaultAsync(x => x.Subject == thread.Subject)!)!.Id;
            return new ThreadInfoDto()
            {
                Id = threadId,
                SubCattegoryId = thread.SubCattegoryId,
                SubCattegorySlug = thread.SubCattegory.Slug,
                CurrentPage = 1,
            };
        }
    }
}
