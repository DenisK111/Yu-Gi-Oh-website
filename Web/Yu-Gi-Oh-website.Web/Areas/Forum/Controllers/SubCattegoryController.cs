using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yu_Gi_Oh_website.Common.Settings;
using Yu_Gi_Oh_website.Services.Forum.Contracts;
using Yu_Gi_Oh_website.Web.Areas.Forum.Models;
using Yu_Gi_Oh_website.Web.Extentension;
using Yu_Gi_Oh_website.Web.Helpers;

namespace Yu_Gi_Oh_website.Web.Areas.Forum.Controllers
{
    [Area("Forum")]
    public class SubCattegoryController : Controller
    {
        private readonly int itemsToTake = WebConstants.ForumThreadsToTake;
        private readonly ISubCattegoryService subCattegoryService;
        private readonly IThreadService threadService;
        private readonly IMapper mapper;

        public SubCattegoryController(ISubCattegoryService subCattegoryService,IThreadService threadService,IMapper mapper)
        {
            this.subCattegoryService = subCattegoryService;
            this.threadService = threadService;
            this.mapper = mapper;
        }
        [HttpGet]
        [Route("{area}/Cattegory/{id:int}/{slug?}")]
        public async Task<IActionResult> Index(int id,int currentPage)
        {
            if (!ModelState.IsValid)
            {
                return this.View("error404");
            }

            currentPage = Paging.PageCheck(currentPage);
            var resultModel = await subCattegoryService.GetByIdAsync(id, currentPage, itemsToTake);
            var model = mapper.Map<FullSubCattegoryViewModel>(resultModel);           
            Paging.CreatePaging(model, resultModel.TotalCount, itemsToTake, currentPage);
            return this.View(model);
        }

        [Authorize]
        [HttpGet]
        [Route("/{area}/Cattegory/{id:int}/{slug?}/Create-Thread")]
        public async Task<IActionResult> CreateThread([FromRoute]int id)
        {
            var viewModel = new CreateThreadInputViewModel()
            {
                Author = this.GetUserEmail()!,
                SubCattegoryId = id
            };
            return await Task.FromResult(this.View(viewModel));
        }

        [Authorize]
        [HttpPost]
        [Route("/{area}/Cattegory/{id:int}/{slug?}/Create-Thread")]
        public async Task<IActionResult> CreateThread(CreateThreadInputViewModel thread)
        {
            if (!ModelState.IsValid)
            {
                return this.View(thread);
            }
            
            var threadToDisplay = await threadService.CreateThread(thread.Subject, thread.PostContent, thread.Author, thread.SubCattegoryId);

            if (threadToDisplay.IsError)
            {
                ModelState.AddModelError("", threadToDisplay.ErrorMessage!);
                return this.View(thread);
            }

            return this.RedirectToAction("Thread","Thread", threadToDisplay);
        }

    }
}
