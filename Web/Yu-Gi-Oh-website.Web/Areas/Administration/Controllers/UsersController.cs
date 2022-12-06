using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Yu_Gi_Oh_website.Data.Data.Seeding.Common;
using Yu_Gi_Oh_website.Models;
using Yu_Gi_Oh_website.Services.Forum.Contracts;
using Yu_Gi_Oh_website.Web.Areas.Administration.Models;

namespace Yu_Gi_Oh_website.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = Roles.Admin)]
    public class UsersController : Controller
	{
        private readonly IUserService userService;
        private readonly RoleManager<ApplicationRole> roleManager;

        public UsersController(IUserService userService,RoleManager<ApplicationRole> roleManager)
        {
            this.userService = userService;
            this.roleManager = roleManager;
        }

        [HttpGet]

        public async Task<IActionResult> Users()
        {
            var users = await userService.GetAllUsersWithRolesAsync();
            return this.View(users);
        }

        [HttpGet]

        public async Task<IActionResult> ModifyRoles(string id)
        {
            
            var user = await userService.GetUserWithRolesAsync(id);

            if (user is null) return this.View("error404");            

            var roles = await roleManager.Roles.Select(x=>x.Name).ToListAsync();

            var modelToReturn = new UserRoleModifierViewModel()
            {
                User = user,
                AllRoles = roles,
            };

            return this.View(modelToReturn);
        }
    }
}
