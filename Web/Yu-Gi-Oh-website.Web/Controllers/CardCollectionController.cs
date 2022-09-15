using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

using Yu_Gi_Oh_website.Services.Contracts;
using Yu_Gi_Oh_website.Services.Models;
using Yu_Gi_Oh_website.Web.Models;

namespace Yu_Gi_Oh_website.Web.Controllers
{
    public class CardCollectionController : Controller
    {
        private readonly ICardCollectionService service;
        private readonly IMapper mapper;
        private readonly IFilterService filter;

        public CardCollectionController(ICardCollectionService service, IMapper mapper, IFilterService filter)
        {
            this.service = service;
            this.mapper = mapper;
            this.filter = filter;
        }
        //[Route("CardCollection/{page:int}")]
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery]FilterViewModel fm)
        {

            bool applyFilter = true;
            if (fm.Fe == null)
            {
                fm.Fe = filter.GetFilterEntries();

                if (fm.SearchTerm == String.Empty)
                {
                    applyFilter = false;
                }

            }

            if (!ModelState.IsValid)
            {
                fm.Page = 1;
            }

            var filterParameters = applyFilter
                ? fm.Fe.SelectMany(x => x.Value).Where(x => x.IsChecked).Select(x => x.Name).ToArray()
                : new string[0];

            var cardModel = await service.GetCards(
                fm.Page - 1,
                fm.SearchTerm!,
                filterParameters,
                applyFilter);
            var cardDisplayModel = mapper.Map<List<CardDisplayViewModel>>(cardModel);



            var viewModel = new CardCollectionViewModel(cardDisplayModel, fm);

            return this.View(viewModel);
        }

        public async Task<IActionResult> Details(int Id)
        {
            var model = await service.GetCard(Id);
            var viewModel = mapper.Map<CardViewModel>(model);
            return this.View(viewModel);
        }






    }
}
