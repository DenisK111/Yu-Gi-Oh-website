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
using Yu_Gi_Oh_website.Services.ExtensionMethods;

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
            [FilterTypesEnum.Level_Rank_Link] = Enumerable.Range(0, 14).Select(x => x.ToString()).ToHashSet(),

        };

        private readonly Dictionary<FilterTypesEnum, Func<HashSet<string>, IQueryable<Card>, IQueryable<Card>>> filterFuncs = new()
        {
            [FilterTypesEnum.Card_Type] = (parameters, query) => query.Where(x => parameters.Contains(x.CardType) || (parameters.Contains("Pendulum") && x.Scale != null)),
            [FilterTypesEnum.Attribute] = (parameters, query) => query.Where(x => x.CardAttribute != null && parameters.Contains(x.CardAttribute!.Name)),
            [FilterTypesEnum.Spell_Trap_Type] = (parameters, query) => query.Where(x => parameters.Contains(x.Type.Name)),
            [FilterTypesEnum.Type] = (parameters, query) => query.Where(x => parameters.Contains(x.Type.Name)),
            [FilterTypesEnum.Level_Rank_Link] = (parameters, query) => query
            .Where(x => (x.Level != null
            &&
            parameters.Contains(x.Level))
            ||
            ( x.LinkValue != null
             &&
            parameters.Contains(x.LinkValue)))

        };
               
        public Dictionary<FilterTypesEnum, List<FilterEntryModel>> GetFilterEntries()
        {
            var model = new Dictionary<FilterTypesEnum, List<FilterEntryModel>>();

            foreach (var (key, value) in filterTypes)
            {
                model.Add(key, new List<FilterEntryModel>());

                foreach (var name in value)
                {
                    model[key].Add(new FilterEntryModel() { Name = name });
                }
            }

            return model;
        }

        public IQueryable<Card> Search(IQueryable<Card> query, string name, ICollection<string> arguments)
        {
            var parameters = new HashSet<string>(arguments);
                
            query = query.Where(x => x.Name.Contains(name));

            if (!parameters.Any())
            {
                return query;
            }

            Dictionary<FilterTypesEnum, HashSet<string>> dict = GetQueryParametersPerFilterType(parameters);
                   

            for (int i = 0; i < dict.Count; i++)
            {

                if (filterTypes[(FilterTypesEnum)i].Intersect(dict[(FilterTypesEnum)i]).Any())
                {
                    query = filterFuncs[(FilterTypesEnum)i].Invoke(dict[(FilterTypesEnum)i], query);

                }
            }

            return query;


        }

        private Dictionary<FilterTypesEnum, HashSet<string>> GetQueryParametersPerFilterType(HashSet<string> parameters)
        {
            var cardTypeParameters = parameters.Where(x => filterTypes[FilterTypesEnum.Card_Type].Contains(x)).ToHashSet();
            var attributeParameters = parameters.Where(x => filterTypes[FilterTypesEnum.Attribute].Contains(x)).ToHashSet();
            var spellTrapTypeParameters = parameters.Where(x => filterTypes[FilterTypesEnum.Spell_Trap_Type].Contains(x)).ToHashSet();
            var monsterTypeParameters = parameters.Where(x => filterTypes[FilterTypesEnum.Type].Contains(x)).ToHashSet();
            var levelRankLinkParameters = parameters.Where(x => filterTypes[FilterTypesEnum.Level_Rank_Link].Contains(x)).ToHashSet();

            var dict = new Dictionary<FilterTypesEnum, HashSet<string>>()
            {
                [FilterTypesEnum.Card_Type] = cardTypeParameters,
                [FilterTypesEnum.Attribute] = attributeParameters,
                [FilterTypesEnum.Spell_Trap_Type] = spellTrapTypeParameters,
                [FilterTypesEnum.Type] = monsterTypeParameters,
                [FilterTypesEnum.Level_Rank_Link] = levelRankLinkParameters,
            };

            return dict;
        }

    
    }
}
