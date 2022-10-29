using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Yu_Gi_Oh_website.Services.Forum.Contracts;
using Yu_Gi_Oh_website.Web.Areas.Forum.Models;

namespace Yu_Gi_Oh_website.Web.Areas.Forum.Controllers
{
    [Area("Forum")]
    [Route("{area}/Cattegory/{subCattegoryId:int}/{subCattegorySlug}/Thread/")]
    public class ThreadController : Controller
    {
        private readonly int forumPostsToTake = 15;
        private readonly IThreadService threadService;
        private readonly IMapper mapper;

        public ThreadController(IThreadService threadService, IMapper mapper)
        {
            this.threadService = threadService;
            this.mapper = mapper;
        }
        [Route("{id:int}")]
        
        public async Task<IActionResult> Thread(int id, int currentPage)
        {
            if (currentPage<1)
            {
                currentPage = 1;
            }

            var threadDetails = await threadService.GetThreadDtoById(id,forumPostsToTake,currentPage-1);

            var model = mapper.Map<ThreadViewModel>(threadDetails.thread);

            var postsCount = threadDetails.postCount;
            var pagesCount = (int)Math.Ceiling(postsCount / (decimal)forumPostsToTake);

            if (model.Paging == null)
            {
                model.Paging = new();
            }

            model.Paging.CurrentPage = currentPage;
            model.Paging.PagesCount = pagesCount;
            model.Paging.ItemsCount = postsCount;

            return this.View(model);
        }        
        
    }
}
