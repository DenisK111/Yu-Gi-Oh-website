using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
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
        private readonly IToastNotification toastNotification;

        public UsersController(IUserService userService, RoleManager<ApplicationRole> roleManager, IToastNotification toastNotification)
        {
            this.userService = userService;
            this.roleManager = roleManager;
            this.toastNotification = toastNotification;
        }

        [HttpGet]

        public async Task<IActionResult> Users()
        {
            var users = await userService.GetAllUsersWithRolesAsync();
            return this.View(users);
        }

        [HttpGet]        
        public async Task<IActionResult> ModifyRoles([FromRoute]string id)
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

        [HttpPost]

        public async Task<IActionResult> AddRole(RoleViewModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    toastNotification.AddErrorToastMessage(error.ErrorMessage);
                }

                return this.RedirectToAction(nameof(ModifyRoles));
            }

            var added = await userService.AddUserToRoleAsync(inputModel.UserId, inputModel.Role);

            if (added)
            {
                toastNotification.AddSuccessToastMessage($"User {inputModel.UserId} has been succesffully added to role {inputModel.Role}");

            }

            else
            {
                toastNotification.AddErrorToastMessage($"Failed to add user to role. User or role does not exist");
            }

            return this.RedirectToAction(nameof(Users));
        }

        [HttpPost]

        public async Task<IActionResult> RemoveRole(RoleViewModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    toastNotification.AddErrorToastMessage(error.ErrorMessage);
                }

                return this.RedirectToAction(nameof(ModifyRoles));
            }

            var added = await userService.RemoveUserFromRoleAsync(inputModel.UserId, inputModel.Role);

            if (added)
            {
                toastNotification.AddSuccessToastMessage($"User {inputModel.UserId} has been succesffully removed from role {inputModel.Role}");

            }

            else
            {
                toastNotification.AddErrorToastMessage($"Failed to remove user from role. User or role does not exist");
            }

            return this.RedirectToAction(nameof(Users));
        }
    }
}
