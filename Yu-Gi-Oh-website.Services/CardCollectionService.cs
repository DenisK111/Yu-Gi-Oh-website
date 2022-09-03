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

namespace Yu_Gi_Oh_website.Services
{
    public class CardCollectionService : ICardCollectionService
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public CardCollectionService(ApplicationDbContext context,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
            
        }

        public async Task<IEnumerable<CardDto>> GetAllCards()
        {
            var result = await mapper.ProjectTo<CardDto>(context.Cards)
                .ToListAsync();

            return result;
        }
    }
}
