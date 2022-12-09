using Microsoft.AspNetCore.Mvc;
using Yu_Gi_Oh_website.Services.Forum.Contracts;

namespace Yu_Gi_Oh_website.Web.Areas.Forum.Controllers
{
    [Area("Forum")]
    public class HomeController : Controller
    {
        private readonly ICattegoryService homeService;

        public HomeController(ICattegoryService homeService)
        {
            this.homeService = homeService;
        }
        public async Task<IActionResult> Index()
        {
            var cattegories = await homeService.GetallCattegoriesAsync(); 
            return this.View(cattegories);
        }


    }
}
