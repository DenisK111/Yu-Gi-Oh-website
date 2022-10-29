using AspNetCoreTemplate.Data.Seeding;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Yu_Gi_Oh_website.Common;
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
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var role = await roleManager.FindByNameAsync("Admin");
            var newUser = new ApplicationUser();
            newUser.UserName = "abc@abc";
            var applicationRole = new ApplicationRole();
            
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>()!;
            await userManager.CreateAsync(newUser, "123456");
            await userManager.AddToRoleAsync(newUser, "Admin");
            var threadsToAdd = new List<ForumThread>();

            for (int i = 1; i <= 50; i++)
            {
                var thread = new ForumThread() { Author = newUser, SubCattegory = subCattegory, Subject = $"Test Thread {i}" };
                thread.Slug = thread.Subject.ToUrlSlug();
                threadsToAdd.Add(thread);
            }

            await dbContext.AddRangeAsync(threadsToAdd);

        }
    }
}
