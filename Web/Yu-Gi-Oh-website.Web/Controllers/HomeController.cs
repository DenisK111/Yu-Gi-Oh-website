using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Yu_Gi_Oh_website.Services.ApiService;
using Yu_Gi_Oh_website.Services.Contracts;
using Yu_Gi_Oh_website.Web.Models;

namespace Yu_Gi_Oh_website.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly string imageFolder = "wwwroot/Images";
        private readonly ILogger<HomeController> _logger;
        private readonly IGetApiDataAndUpdateDbService updater;
        private readonly ICardCollectionService service;

        public HomeController(ILogger<HomeController> logger, IGetApiDataAndUpdateDbService updater,ICardCollectionService service)
        {
            _logger = logger;
            this.updater = updater;
            this.service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            this.HttpContext.Session.SetString("Read Privacy", "true");
            return this.View();
        }

        public IActionResult AddToDb(AddToDbModel model)
        {

            var dbModel = model ?? new AddToDbModel();

            return this.View(dbModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddCardToDb(AddToDbModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(AddToDb),model);
            }

            await updater.AddIndividualCardToDbAsync(imageFolder, model.cardName);
            return this.Redirect(nameof(Index));
        }

       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("/update")]
        public async Task<IActionResult> UpdateDb()
        {
            
            await updater.AddAllCardsToDbAsync(imageFolder);

            return RedirectToAction(nameof(this.Index));
        }
    }
}