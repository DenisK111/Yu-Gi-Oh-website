using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yu_Gi_Oh_website.Models.CardCatalogue.Models;
using Yu_Gi_Oh_website.Services.Common.Enums;
using Yu_Gi_Oh_website.Services.Contracts;

namespace Yu_Gi_Oh_website.Services.Implementations
{
    public class SortingService : ISortingService
    {
        private Dictionary<SortTypeEnum, Func<IQueryable<Card>, IQueryable<Card>>> sortingFunctions = new Dictionary<SortTypeEnum, Func<IQueryable<Card>, IQueryable<Card>>>()
        {

            
            [SortTypeEnum.A_Z] = x => x.OrderBy(y=>y.Name),
            [SortTypeEnum.Z_A] = x => x.OrderByDescending(y=>y.Name),
            [SortTypeEnum.ATKASC] = x => x.OrderBy(y=>y.Atk),
            [SortTypeEnum.ATKDESC] = x => x.OrderByDescending(y=>y.Atk),
            [SortTypeEnum.DEFASC] = x => x.OrderBy(y=>y.Def),
            [SortTypeEnum.DEFDESC] = x => x.OrderByDescending(y=>y.Def),
            [SortTypeEnum.LEVEL_RANK_LINK_ASC] = x => x.OrderBy(y => Convert.ToInt32(y.Level + y.LinkValue)),
            [SortTypeEnum.LEVEL_RANK_LINK_DESC] = x => x.OrderByDescending(y => Convert.ToInt32(y.Level + y.LinkValue)),
        };
        private readonly ILogger logger;

        public SortingService(ILogger<SortingService> logger)
        {
            this.logger = logger;
        }

        public SortTypeEnum[] GetSortings()
        {
            return Enum.GetValues<SortTypeEnum>();
        }

        public IQueryable<Card> Sort(IQueryable<Card> query, SortTypeEnum key)
        {
            return sortingFunctions[key].Invoke(query);
        }

       
    }
}
