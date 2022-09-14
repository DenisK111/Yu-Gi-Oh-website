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
using Yu_Gi_Oh_website.Web.Data;
using Yu_Gi_Oh_website.Services.ApiService;
using Yu_Gi_Oh_website.Services.Common.Enums;

namespace Yu_Gi_Oh_website.Services.Implementations
{
    public class FilterService : IFilterService
    {
        private readonly Dictionary<FilterTypesEnum, HashSet<string>> filterTypes = new()
        {
            [FilterTypesEnum.Card_Type] = Enum.GetValues<CardTypeEnum>().Skip(1).Select(x => x.ToString()).ToHashSet(),
            [FilterTypesEnum.Attribute] = ApiConstantValues.attributes,
            [FilterTypesEnum.Spell_Trap_Type] = ApiConstantValues.spellTrapTypes,
            [FilterTypesEnum.Type] = ApiConstantValues.monsterTypes,
            [FilterTypesEnum.Level_Rank_Link] = Enumerable.Range(0,14).Select(x=>x.ToString()).ToHashSet(),
            
        };
             

        

        public Dictionary<FilterTypesEnum, List<FilterEntryModel>> GetFilterEntries()
        {
            var model = new Dictionary<FilterTypesEnum, List<FilterEntryModel>>();

            foreach (var (key,value) in filterTypes)
            {
                model.Add(key, new List<FilterEntryModel>());

                foreach(var name in value)
                {
                    model[key].Add(new FilterEntryModel() { Name = name });
                }
            }

            return model;
        }

        public IQueryable<Card> Search(IQueryable<Card> query, string name, string[] parameters)
        {
            query = query.Where(x => x.Name.Contains(name));

            if (!parameters.Any())
            {
                return query;
            }

            var queryBuilder = new HashSet<CardTypeEnum>();

            foreach (var parameter in parameters)
            {
                queryBuilder.Add(Enum.Parse<CardTypeEnum>(parameter));
                //  query = filter[parameter].Invoke(query);
            }


            query = query.Where(x => queryBuilder.Contains(x.CardType));

            return query;


        }


    }
}
