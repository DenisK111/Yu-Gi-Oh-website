using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Yu_Gi_Oh_website.Models.CardCatalogue.Models;
using Yu_Gi_Oh_website.Services.Contracts;
using Yu_Gi_Oh_website.Services.Models;
using Yu_Gi_Oh_website.Web.Data;

namespace Yu_Gi_Oh_website.Services.Implementations
{
    public class CardCollectionService : ICardCollectionService
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IFilterService filter;

        public CardCollectionService(ApplicationDbContext context, IMapper mapper, IFilterService filter)
        {
            this.context = context;
            this.mapper = mapper;
            this.filter = filter;
        }
             
        private IQueryable<Card> Filter(IQueryable<Card> expression, string name)
        {
            return filter.Search(expression, name);
        }

        public async Task<IEnumerable<CardDisplayDto>> GetCards(uint page, string name)
        {
            int cardsPerPage = 18;
            var result = context.Cards .OrderBy(x => x.Name).AsNoTracking();
            var filteredResult = Filter(result, name);
            var endResult = await mapper.ProjectTo<CardDisplayDto>(filteredResult)
            .Skip((int)page * cardsPerPage)
            .Take(cardsPerPage)
            .ToListAsync();
            return endResult;
        }

        private void CheckNull<T>(T input)
            where T : new()
        {
            if (input is null)
            {
                input = new T();
            }

        }

        public async Task<CardDto> GetCard(string Id)
        {
            var result = mapper.ProjectTo<CardDto>(context.Cards.Where(x => x.Id == Id));
            return await result.FirstAsync();
        }


    }
}
