using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<IActionResult> CardCollection()
        {
            var model = await service.GetAllCards();
            var viewModel = mapper.Map<CardDisplayViewModel>(model);
            return this.View(viewModel);
        }

        public async Task<IActionResult> Details(string Id)
        {
            var model = await service.GetCard(Id);
            var viewModel = mapper.Map<CardViewModel>(model);
            return this.View(viewModel);
        }
    }
}
