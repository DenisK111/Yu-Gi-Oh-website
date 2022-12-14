using Yu_Gi_Oh_website.Models.CardCatalogue.Models;
using Yu_Gi_Oh_website.Services.Common.Enums;
using Yu_Gi_Oh_website.Services.Models;


namespace Yu_Gi_Oh_website.Services.Contracts
{
    public interface IFilterService
    {
        IQueryable<Card> Search(IQueryable<Card> query, string name, ICollection<string> parameters);
        Dictionary<FilterTypesEnum, List<FilterEntryModel>> GetFilterEntries();
    }
}
