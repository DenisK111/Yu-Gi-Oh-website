using Microsoft.AspNetCore.Mvc;

namespace Yu_Gi_Oh_website.Web.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statuscode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    return View("error404");
                case 500:
                    return View("error500");

            }

            return View("Error");
        }
    }
}
