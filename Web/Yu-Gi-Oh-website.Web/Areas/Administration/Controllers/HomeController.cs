using Microsoft.AspNetCore.Mvc;

namespace Yu_Gi_Oh_website.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    public class HomeController : Controller
	{
        public async Task<IActionResult> Index()
        {
            
            return this.View();
        }
    }
}
