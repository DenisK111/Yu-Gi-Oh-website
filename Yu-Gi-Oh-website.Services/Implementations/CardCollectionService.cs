using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public CardCollectionService(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;

        }

        public async Task<IEnumerable<CardDisplayDto>> GetAllCards()
        {
            var result = await mapper.ProjectTo<CardDisplayDto>(context.Cards)
                .ToListAsync();

            return result;
        }

        public async Task<CardDto> GetCard(string Id)
        {
            var result = mapper.ProjectTo<CardDto>(context.Cards.Where(x => x.Id == Id));
            return await result.FirstAsync();
        }
    }
}
