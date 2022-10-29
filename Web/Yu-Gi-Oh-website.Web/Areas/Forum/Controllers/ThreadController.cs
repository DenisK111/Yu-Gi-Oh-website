using Microsoft.AspNetCore.Mvc;
using Yu_Gi_Oh_website.Services.Forum.Contracts;

namespace Yu_Gi_Oh_website.Web.Areas.Forum.Controllers
{
    [Area("Forum")]
    public class ThreadController : Controller
    {
        private readonly IThreadService threadService;

        public ThreadController(IThreadService threadService)
        {
            this.threadService = threadService;
        }
        [Route("{area}/Cattegory/{subCattegoryId:int}/{subCattegoryName}/Thread/{id:int}")]
        
        public async Task<IActionResult> Thread(int id)
        {
            var thread = await threadService.GetThreadDtoById(id);

            return this.View(thread);
        }        
        
    }
}
