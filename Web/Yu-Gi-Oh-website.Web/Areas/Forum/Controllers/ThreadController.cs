using Microsoft.AspNetCore.Mvc;

namespace Yu_Gi_Oh_website.Web.Areas.Forum.Controllers
{
    [Area("Forum")]
    [Route("{area}/Cattegory/{subCattegoryId:int}/{subCattegoryName}")]
    public class ThreadController : Controller
    {
        [Route("Thread/{id:int}")]
        public IActionResult Thread(int id)
        {
            return NotFound();
        }
    }
}
