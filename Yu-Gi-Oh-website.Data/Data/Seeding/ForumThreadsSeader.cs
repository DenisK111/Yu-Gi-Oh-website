using System;
using AspNetCoreTemplate.Data.Seeding;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Yu_Gi_Oh_website.Common;
using Yu_Gi_Oh_website.Data.Data.Seeding.Common;
using Yu_Gi_Oh_website.Models;
using Yu_Gi_Oh_website.Models.Forum.Models;
using Yu_Gi_Oh_website.Web.Data;

namespace Yu_Gi_Oh_website.Data.Data.Seeding
{
    public class ForumThreadsSeader : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Threads.Any())
            {
                return;
            }

            var subCattegory = dbContext.SubCattegories.First();

            var admin = await CreateAdmin(serviceProvider);
            var threadsToAdd = new List<ForumThread>();

            for (int i = 1; i <= 20; i++)
            {
                var thread = new ForumThread() { Author = admin, SubCattegory = subCattegory, Subject = $"Test Thread {i}" };
                thread.Slug = thread.Subject.ToUrlSlug();
                threadsToAdd.Add(thread);
            }

            await dbContext.AddRangeAsync(threadsToAdd);

        }

        private async Task<ApplicationUser> CreateAdmin(IServiceProvider serviceProvider)
        {
            var newUser = new ApplicationUser();
            newUser.UserName = InitialSeedSettings.AdminUserName;
            newUser.Email = InitialSeedSettings.AdminUserName;
            newUser.NormalizedEmail = InitialSeedSettings.AdminUserName.ToUpper();
            newUser.PostCount = InitialSeedSettings.InititalPostCount;
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>()!;
            await userManager.CreateAsync(newUser, InitialSeedSettings.AdminUserPassword);
            await userManager.AddToRoleAsync(newUser, Roles.Admin);
            return newUser;
        }
    }
}
