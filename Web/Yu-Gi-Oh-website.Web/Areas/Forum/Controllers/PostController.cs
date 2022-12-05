using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NToastNotify;
using Yu_Gi_Oh_website.Data.Data.Seeding.Common;
using Yu_Gi_Oh_website.Services.Forum.Contracts;
using Yu_Gi_Oh_website.Services.Forum.Implementations;
using Yu_Gi_Oh_website.Web.Areas.Forum.Models;


namespace Yu_Gi_Oh_website.Web.Areas.Forum.Controllers
{
    [Area("Forum")]
    [Authorize]
    public class PostController : Controller
    {
        private readonly IPostService postService;
        private readonly IMapper mapper;
        private readonly IToastNotification toastNotification;

        public PostController(IPostService postService, IMapper mapper, IToastNotification toastNotification)
        {
            this.postService = postService;
            this.mapper = mapper;
            this.toastNotification = toastNotification;
        }

        [HttpGet]
        public IActionResult AddPost(int id)
        {
            var viewModel = new AddPostInputViewModel()
            {
                Author = this.User.Identity!.Name!,
                ThreadId = id
            };
            return this.View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> AddPost(AddPostInputViewModel post)
        {
            if (!ModelState.IsValid)
            {
                return this.View(post);
            }

            var threadToDisplay = await postService.AddPost(post.ThreadId, post.PostContent, post.Author);

            if (threadToDisplay.IsError)
            {
                ModelState.AddModelError("", threadToDisplay.ErrorMessage!);
                return this.View(post);
            }

            var model = mapper.Map<ThreadInfoViewModel>(threadToDisplay);

            return this.RedirectToAction("Thread", "Thread", model);
        }

        [HttpPost]
        [Authorize(Roles = $"{Roles.Admin},{Roles.Moderator}")]
        public async Task<IActionResult> RemovePost(int postId, int threadId)
        {
            var removed = await postService.RemovePost(postId);
            var model = mapper.Map<ThreadInfoViewModel>(removed);

            TempData["Removed"] = removed is not null;

            if (removed is not null)
            {
                toastNotification.AddSuccessToastMessage("Post Removed Successfully!");
               
            }

            else toastNotification.AddErrorToastMessage("Failed to remove post.");


            return this.RedirectToAction("Thread", "Thread", model);
        }
    }
}
