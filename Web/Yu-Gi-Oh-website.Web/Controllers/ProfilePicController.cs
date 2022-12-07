using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using Yu_Gi_Oh_website.Models;
using Yu_Gi_Oh_website.Services.Contracts;
using Yu_Gi_Oh_website.Services.Forum.Contracts;
using Yu_Gi_Oh_website.Web.Extentension;
using Yu_Gi_Oh_website.Web.Models.ProfilePic;

namespace Yu_Gi_Oh_website.Web.Controllers
{
    
    [Authorize]
    public class ProfilePicController : Controller

    {
        private readonly IImageUploadService imageUploadService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserService userService;
        private readonly IToastNotification toast;

        public ProfilePicController(IImageUploadService imageUploadService, UserManager<ApplicationUser> userManager, IUserService userService, IToastNotification toast)
        {
            this.imageUploadService = imageUploadService;
            this.userManager = userManager;
            this.userService = userService;
            this.toast = toast;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = this.GetUserId();
            var user = await userManager.Users.Where(x => x.Id == userId).FirstOrDefaultAsync()!;            
            var model = new ProfilePicViewModel()
            {               
                ProfilePic = user!.ProfilePic,

            };
            this.ViewData["Picture"] = true;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(ProfilePicViewModel inputModel)
        {

            if (!ModelState.IsValid)
            {
                return this.View(inputModel);
            }
            var userId = this.GetUserId();
            var imageUrl = await imageUploadService.UploadAsync(inputModel.NewProfilePic);
            var result = await userService.UpdateProfilePictureAsync(userId!, imageUrl);

            if (result)
            {
                toast.AddSuccessToastMessage("Profile Picture has been successfully Changed.");
            }

            else toast.AddErrorToastMessage("Failed to update profile picture.");
            
            return this.RedirectToAction(nameof(Index));
        }

    }
}
