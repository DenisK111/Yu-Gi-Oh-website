﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Yu_Gi_Oh_website.Services.Common.Enums;
using Yu_Gi_Oh_website.Services.Contracts;
using Yu_Gi_Oh_website.Services.Models;
using Yu_Gi_Oh_website.Web.Models;
using Yu_Gi_Oh_website.Web.Models.CardCollection;
using Yu_Gi_Oh_website.Web.Models.CardDetails;

namespace Yu_Gi_Oh_website.Web.Controllers
{
    public class CardCollectionController : Controller
    {
        private readonly int cardsPerPage = 21;
        private readonly ICardCollectionService service;
        private readonly IMapper mapper;
        private readonly IFilterService filter;
        private readonly ISortingService sorter;

        public CardCollectionController(ICardCollectionService service, IMapper mapper, IFilterService filter, ISortingService sorter)
        {
            this.service = service;
            this.mapper = mapper;
            this.filter = filter;
            this.sorter = sorter;
        }
        //[Route("CardCollection/{page:int}")]
        [HttpGet]
        public async Task<IActionResult> Index(FilterViewModel fm)
        {
            if (!ModelState.IsValid)
            {
                fm.Page = 1;
            }

            fm.Filters = HttpContext.Request.Query
                .Where(x => x.Key == "Filters")
                .SelectMany(x => x.Value, (col, value) => new KeyValuePair<string, string>(col.Key, value))
                .Select(x => x.Value)
                .ToArray();

            bool applyFilter = CheckModelParameters(fm);

            var cardModel = await service.GetCardsAndCount(
                fm.Sorting,
                fm.SearchTerm!,
                fm.Filters!,
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
                CardModel = cardDisplayModel,
                CurrentPage = fm.Page,
                PagesCount = pagesCount,
                CardsCount = cardsCount,
            };

            return this.View(viewModel);

        }


        public async Task<IActionResult> Details(int Id)
        {
            var model = await service.GetCard(Id);
            var viewModel = mapper.Map<CardDetailsViewModel>(model);
            return this.View(viewModel);
        }

        private bool CheckModelParameters(FilterViewModel fm)
        {
            bool applyFilter = true;


            if (fm.FilterEntries == null)
            {
                fm.FilterEntries = filter.GetFilterEntries();
                fm.Sortings = sorter.GetSortings();

            }

            if (fm.SearchTerm == String.Empty && fm.Filters is null)
            {
                fm.Filters = new string[0];
                applyFilter = false;
            }

            else if (fm.Filters is null)
            {
                fm.Filters = new string[0];
            }

           

            return applyFilter;
        }





    }
}
