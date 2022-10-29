using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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

		public PostController(IPostService postService)
		{
			this.postService = postService;
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

            var threadToDisplay = await postService.AddPost(post.ThreadId,post.PostContent, post.Author);

            if (threadToDisplay.IsError)
            {
                ModelState.AddModelError("", threadToDisplay.ErrorMessage!);
                return this.View(post);
            }

            return this.RedirectToAction("Thread", "Thread", threadToDisplay);
        }
    }
}
