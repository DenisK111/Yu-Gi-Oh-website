using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net;
using Yu_Gi_Oh_website.Services.Contracts;
using Yu_Gi_Oh_website.Services.Models;
using Yu_Gi_Oh_website.Web.Models;

namespace Yu_Gi_Oh_website.Web.Controllers
{
    public class CardCollectionController : Controller
    {
        private readonly int cardsPerPage = 21;
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
        public async Task<IActionResult> Index(FilterViewModel fm)
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

            var cardModel = await service.GetCardsAndCount(
                fm.SearchTerm!,
                filterParameters,
                applyFilter);

            var cardDisplayModel = mapper
                .Map<List<CardDisplayViewModel>>(await cardModel.cards!
                .Skip((fm.Page - 1) * cardsPerPage)
                .Take(cardsPerPage)
                .ToListAsync());
            
            var cardsCount = cardModel.count;
            var pagesCount = (int)Math.Ceiling(cardsCount / (decimal)cardsPerPage);
            var viewModel = new CardCollectionViewModel
            {
                Fm = fm,
                CardModel= cardDisplayModel,
                CurrentPage = fm.Page,
                PagesCount = pagesCount,
                CardsCount = cardsCount,
                
            };

            

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
