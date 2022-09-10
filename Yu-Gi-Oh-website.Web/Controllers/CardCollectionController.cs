﻿using AutoMapper;
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
        [Route("CardCollection/{page=0}")]
        public async Task<IActionResult> Index(uint page)
        {

            var model = await service.GetAllCards(page);
            var viewModel = mapper.Map<IEnumerable<CardDisplayViewModel>>(model);
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
