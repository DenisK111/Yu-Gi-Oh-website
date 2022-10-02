using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Yu_Gi_Oh_website.Models.CardCatalogue.Models;
using Yu_Gi_Oh_website.Services.Common.Enums;
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
        private readonly ISortingService sorting;
        public static readonly int cardsPerPage = 14;

        public CardCollectionService(ApplicationDbContext context, IMapper mapper, IFilterService filter, ISortingService sorting)
        {
            this.context = context;
            this.mapper = mapper;
            this.filter = filter;
            this.sorting = sorting;
        }

        private IQueryable<Card> Filter(IQueryable<Card> expression, string name, string[] parameters)
        {
            return filter.Search(expression, name, parameters);
        }

        public async Task<(IQueryable<CardDisplayDto>? cards, int count)> GetCardsAndCount(SortTypeEnum sortKey, string name, string[] parameters, bool applyFilter)
        {

            IQueryable<Card> result = context.Cards.AsNoTracking();
            result = applyFilter ? Filter(result, name, parameters) : result;
            result = sorting.Sort(result, sortKey);
            IQueryable<CardDisplayDto>? endResult = mapper.ProjectTo<CardDisplayDto>(result);
            return (endResult, await result.CountAsync());
        }

        private void CheckNull<T>(T input)
            where T : new()
        {
            if (input is null)
            {
                input = new T();
            }

        }

        public async Task<CardDto> GetCard(int id)
        {
            var result = mapper.Map<CardDto>(await
                context.Cards
                .Include(x => x.ExactCardType)
                .Include(x => x.CardImages) 
                .Include(x => x.CardAttribute)
                .Include(x=>x.Type)
                .FirstOrDefaultAsync(x => x.Id == id));
            return result;
        }


    }
}
