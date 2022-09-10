using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Yu_Gi_Oh_website.Services.Contracts;
using Yu_Gi_Oh_website.Services.Models;

namespace Yu_Gi_Oh_website.Web.Controllers
{
    public class CardCollectionController : Controller
    {
        private readonly ICardCollectionService service;
        private readonly IMapper mapper;

        public CardCollectionController(ICardCollectionService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }
        [Route("CardCollection/{page:int}")]
        public async Task<IActionResult> Index(uint page)
        {

            var model = await service.GetCards(page,"");
            var viewModel = mapper.Map<IEnumerable<CardDisplayViewModel>>(model);
            return this.View(viewModel);
        }

        public async Task<IActionResult> Details(string Id)
        {
            var model = await service.GetCard(Id);
            var viewModel = mapper.Map<CardViewModel>(model);
            return this.View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Search(uint page, string name = "")
        {


            var model = await service.GetCards(page, name);
            var viewModel = mapper.Map<IEnumerable<CardDisplayViewModel>>(model);
            return this.View(nameof(Index),viewModel);
        }
    }
}
