using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public ThreadService(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
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
            var author = await GetAuthorByUserName(authorName);

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

            return await AddPost(thread, postContent, author);

        }      

        public async Task<ThreadDto?> GetThreadDtoById(int id)
        {
            var thread = await context.Threads
                .Include(x=>x.Author)
                .Include(x=>x.Posts)
                .ThenInclude(x=>x.PostContent)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (thread == null)
            {
                return null;
            }

            var threadDto = mapper.Map<ThreadDto>(thread);
            return threadDto;
        }
        public async Task<ThreadInfoDto> AddPost(int threadId, string postContent, string authorName)
        {
            var thread = await GetThreadById(threadId);

            if (thread == null) return new ThreadInfoDto()
                {
                    IsError = true,
                    ErrorMessage = "Post must belong to an existing Thread."
                };
            
            var author = await GetAuthorByUserName(authorName);

            if (author == null) return new ThreadInfoDto()
            {
                IsError = true,
                ErrorMessage = "Post must have an Author."
            };            

            return await this.AddPost(thread, postContent, author);
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

            await context.SaveChangesAsync();

            var threadId = (await context.Threads.FirstOrDefaultAsync(x => x.Subject == thread.Subject)!)!.Id;
            return new ThreadInfoDto()
            {
                Id = threadId,
                SubCattegoryId = thread.SubCattegoryId,
                SubCattegoryName = thread.SubCattegory.Name,
            };


        }
        private async Task<ForumThread?> GetThreadById(int threadId)
        {
            var thread = await context.Threads
                .Include(x => x.Author)
                .Include(x=>x.SubCattegory)
                .Include(x => x.Posts)
                .ThenInclude(x => x.PostContent)
                .FirstOrDefaultAsync(x => x.Id == threadId);

            return thread;
        }

        private async Task<ApplicationUser?>  GetAuthorByUserName(string userName)
        {
            return await context.Users.FirstOrDefaultAsync(x => x.UserName == userName);
        }
    }
}
