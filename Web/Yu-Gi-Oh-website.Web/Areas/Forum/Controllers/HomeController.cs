using Microsoft.AspNetCore.Mvc;

namespace Yu_Gi_Oh_website.Web.Areas.Forum.Controllers
{
    [Area("Forum")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }


    }
}
