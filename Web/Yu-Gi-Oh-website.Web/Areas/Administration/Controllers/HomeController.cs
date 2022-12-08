using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yu_Gi_Oh_website.Data.Data.Seeding.Common;

namespace Yu_Gi_Oh_website.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = Roles.Admin)]
    public class HomeController : Controller
	{
        public async Task<IActionResult> Index()
        {
            
            return this.View();
        }
    }
}
