using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Yu_Gi_Oh_website.Common;
using Yu_Gi_Oh_website.Models;
using Yu_Gi_Oh_website.Models.Forum.Models;
using Yu_Gi_Oh_website.Services.Forum.Contracts;
using Yu_Gi_Oh_website.Services.Forum.Models;
using Yu_Gi_Oh_website.Web.Data;

namespace Yu_Gi_Oh_website.Services.Forum.Implementations
{
    public class ThreadService : IThreadService
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IPostService postService;
        private readonly IEntityByIdService entityService;

        public ThreadService(ApplicationDbContext context, IMapper mapper, IPostService postService, IEntityByIdService entityService)
        {
            this.context = context;
            this.mapper = mapper;
            this.postService = postService;
            this.entityService = entityService;
        }
        public async Task<ThreadInfoDto> CreateThread(string subject, string postContent, string authorName, int subCattegoryId)
        {
            if (context.Threads.Any(x => x.Subject == subject))
            {
                return new ThreadInfoDto()
                {
                    IsError = true,
                    ErrorMessage = "A Thread with this name already exists",
                };
            }
            var author = await entityService.GetAuthorByUserName(authorName);

            if (author == null)
            {
                return new ThreadInfoDto()
                {
                    IsError = true,
                    ErrorMessage = "No Such User exists in the Database",
                };
            }

            var subCattegory = await context.SubCattegories.FirstOrDefaultAsync(x => x.Id == subCattegoryId);

            if (subCattegory == null)
            {
                return new ThreadInfoDto()
                {
                    IsError = true,
                    ErrorMessage = "No Such SubCattegory Exists",
                };
            }

            var thread = new ForumThread()
            {
                Author = author,
                Subject = subject,
                SubCattegory = subCattegory,
                Slug = subject.ToUrlSlug(),

            };

            subCattegory.Threads.Add(thread);

            return await postService.AddPost(thread.Id, postContent, author.UserName);

        }

        public async Task<ThreadDto?> GetThreadDtoById(int id)
        {
            var thread = await context.Threads
                .Include(x => x.Author)
                .Include(x => x.Posts)
                .ThenInclude(x=>x.Votes)
                .Include(x=>x.Posts)
                .ThenInclude(x => x.PostContent)                
                .FirstOrDefaultAsync(x => x.Id == id);

            if (thread == null)
            {
                return null;
            }

            var threadDto = mapper.Map<ThreadDto>(thread);
            return threadDto;
        }
    }
}
