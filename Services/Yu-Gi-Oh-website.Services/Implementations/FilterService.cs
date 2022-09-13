using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Yu_Gi_Oh_website.Models.CardCatalogue.Models;
using Yu_Gi_Oh_website.Models.Enums;
using Yu_Gi_Oh_website.Services.Common;
using Yu_Gi_Oh_website.Services.Contracts;
using Yu_Gi_Oh_website.Services.Models;

namespace Yu_Gi_Oh_website.Services.Implementations
{
    public class FilterService : IFilterService
    {
        private static readonly Dictionary<string, Func<IQueryable<Card>, IQueryable<Card>>> filter = new()
        {
            [CardTypeEnum.Normal.ToString()] = x => x.Where(q => q.CardType == CardTypeEnum.Normal)

        };

        public List<FilterEntryModel> GetFilterEntries()
        {
            var model = new List<FilterEntryModel>();

            foreach (var key in filter.Keys)
            {
                model.Add(new FilterEntryModel() { Name = key });
            }

            return model;
        }

        public IQueryable<Card> Search(IQueryable<Card> query, string name, string[] parameters)
        {
           query = query.Where(x => x.Name.Contains(name));

            foreach (var parameter in parameters)
            {
                query = filter[parameter].Invoke(query);
            }

            return query;

            
        }

       
    }
}
