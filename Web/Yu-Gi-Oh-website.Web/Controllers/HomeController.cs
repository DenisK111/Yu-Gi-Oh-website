using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Yu_Gi_Oh_website.Web.Models;

namespace Yu_Gi_Oh_website.Web.Controllers
{
    public class HomeController : Controller
    { 

        public IActionResult Index()
        {
            this.ViewData["Home"] = true;
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}