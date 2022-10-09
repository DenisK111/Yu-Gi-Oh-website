using Microsoft.AspNetCore.Mvc;
using Yu_Gi_Oh_website.Services.Forum.Contracts;
using Yu_Gi_Oh_website.Web.Areas.Forum.Models;

namespace Yu_Gi_Oh_website.Web.Areas.Forum.Controllers
{
    [Area("Forum")]
    public class SubCattegoryController : Controller
    {
        private readonly ISubCattegoryService subCattegoryService;
        private readonly IThreadService threadService;

        public SubCattegoryController(ISubCattegoryService subCattegoryService,IThreadService threadService)
        {
            this.subCattegoryService = subCattegoryService;
            this.threadService = threadService;
        }
        [HttpGet]
        [Route("{area}/Cattegory/{id:int}/{slug?}")]
        public async Task<IActionResult> Index(int id)
        {
            var resultModel = await subCattegoryService.GetByIdAsync(id);
            return this.View(resultModel);
        }

        [HttpGet]
        [Route("/{area}/Cattegory/{id:int}/{slug?}/Create-Thread")]
        public async Task<IActionResult> CreateThread()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateThread(CreateThreadInputViewModel thread)
        {
            if (!ModelState.IsValid)
            {
                return this.View(thread);
            }

            
            var threadToDisplay = await threadService.CreateThread(thread.Subject, thread.PostContent, thread.Author, thread.SubCattegoryId);

            if (threadToDisplay.IsError)
            {
                this.ViewData["error"] = threadToDisplay.ErrorMessage;
            }

            return this.RedirectToAction("Thread","Thread", threadToDisplay);
        }

    }
}
