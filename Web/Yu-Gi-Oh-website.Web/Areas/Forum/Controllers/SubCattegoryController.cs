using Microsoft.AspNetCore.Mvc;
using Yu_Gi_Oh_website.Services.Forum.Contracts;

namespace Yu_Gi_Oh_website.Web.Areas.Forum.Controllers
{
    [Area("Forum")]
    public class SubCattegoryController : Controller
    {
        private readonly ISubCattegoryService subCattegoryService;

        public SubCattegoryController(ISubCattegoryService subCattegoryService)
        {
            this.subCattegoryService = subCattegoryService;
        }
        [HttpGet]
        [Route("{id:int}/{slug}")]
        public async Task<IActionResult> Index(int id)
        {
            var resultModel = await subCattegoryService.GetByIdAsync(id);
            return this.View(resultModel);
        }

    }
}
