﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yu_Gi_Oh_website.Models.Forum.Models;
using Yu_Gi_Oh_website.Models;
using Yu_Gi_Oh_website.Services.Forum.Models;
using Yu_Gi_Oh_website.Services.Forum.Contracts;
using Yu_Gi_Oh_website.Web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Yu_Gi_Oh_website.Services.Forum.Implementations
{
    public class PostService : IPostService
    {
        private readonly ApplicationDbContext context;
        private readonly IEntityByIdService entityService;
        private readonly UserManager<ApplicationUser> userManager;

        public PostService(ApplicationDbContext context, IEntityByIdService entityService, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.entityService = entityService;
            this.userManager = userManager;
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
                currentPage = 1,
            };
        }
    }
}