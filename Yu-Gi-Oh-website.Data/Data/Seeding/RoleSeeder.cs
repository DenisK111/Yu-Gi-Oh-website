namespace AspNetCoreTemplate.Data.Seeding
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Yu_Gi_Oh_website.Models;

    internal class RolesSeeder : ISeeder
    {
       
        public async Task SeedAsync(Yu_Gi_Oh_website.Web.Data.ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            var rolesList = new List<string>()
            {
                "Admin",
                "Moderator",
                "User"
            };

            foreach (var role in rolesList)
            {
                await SeedRoleAsync(roleManager, role);
            }

        }

        private static async Task SeedRoleAsync(RoleManager<ApplicationRole> roleManager, string roleName)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                var result = await roleManager.CreateAsync(new ApplicationRole(roleName));
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
