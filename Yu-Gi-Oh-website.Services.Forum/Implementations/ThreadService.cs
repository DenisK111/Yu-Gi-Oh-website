using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yu_Gi_Oh_website.Common;
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

        public ThreadService(ApplicationDbContext context,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<ThreadInfoDto> CreateThread(string subject, string postContent, string Author, int subCattegoryId)
        {
            if (context.Threads.Any(x => x.Subject == subject))
            {
                return new ThreadInfoDto()
                {
                    IsError = true,
                    ErrorMessage = "A Thread with this name already exists",
                };
            }
            var author = context.Users.FirstOrDefault(x => x.UserName == Author);

            if (author == null)
            {
                return new ThreadInfoDto()
                {
                    IsError = true,
                    ErrorMessage = "No Such User exists in the Database",
                };
            }

            var subCattegory = context.SubCattegories.FirstOrDefault(x => x.Id == subCattegoryId);

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

            var post = new Post()
            {
                Author = author,
                Thread = thread,
                PostContent = new PostContent()
                {
                    Content = postContent
                },
                ParentPost = null,


            };

            thread.Posts.Add(post);
            subCattegory.Threads.Add(thread);

            await context.SaveChangesAsync();

            var threadId = context.Threads.FirstOrDefault(x => x.Subject == subject)!.Id;

            return new ThreadInfoDto()
            {
                Id = threadId,
                SubCattegoryId = subCattegoryId,
                SubCattegoryName = subCattegory.Name,
            };

        }

        public async Task<ThreadDto?> GetThreadById(int id)
        {
            var thread = context.Threads.FirstOrDefault(x => x.Id == id);
            if (thread==null)
            {
                return null;
            }

            var threadDto = mapper.Map<ThreadDto>(thread);
            return threadDto;
        }
    }
}
