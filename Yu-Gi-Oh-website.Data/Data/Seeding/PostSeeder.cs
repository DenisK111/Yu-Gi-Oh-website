using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreTemplate.Data.Seeding;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Yu_Gi_Oh_website.Models.Forum.Models;
using Yu_Gi_Oh_website.Models;
using Yu_Gi_Oh_website.Web.Data;
using Microsoft.EntityFrameworkCore;
using Yu_Gi_Oh_website.Data.Data.Seeding.Common;

namespace Yu_Gi_Oh_website.Data.Data.Seeding
{
    public class PostSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Posts.Any())
            {
                return;
            }

            var threadToAddPostsTo = await dbContext.Threads.Take(20).ToListAsync();

            var postContents = new List<PostContent>();

            for (int i = 0; i < 10; i++)
            {
                var postContent = new PostContent()
                {
                    Content = $"Test Content aawdwad{i}"
                };

                postContents.Add(postContent);
            }

            var random = new Random();
            await dbContext.AddRangeAsync(postContents);

            var posts = new List<Post>();

            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>()!;
            var user = await userManager.FindByNameAsync(InitialSeedSettings.AdminUserName);

            for (int i = 1; i <= InitialSeedSettings.InititalPostCount; i++)
            {
                var content = postContents[random.Next(0, postContents.Count)];

                var post = new Post()
                {
                    PostContent = content,
                    Author = user,
                    Thread = threadToAddPostsTo[random.Next(0,threadToAddPostsTo.Count)]
                };

                posts.Add(post);
            }

            await dbContext.AddRangeAsync(posts);

        }
    }
}
