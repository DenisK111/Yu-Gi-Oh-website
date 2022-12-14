using Yu_Gi_Oh_website.Models.CardCatalogue.Models;
using Yu_Gi_Oh_website.Services.Common.Enums;

namespace Yu_Gi_Oh_website.Services.Contracts
{
    public interface ISortingService
    {
        IQueryable<Card> Sort(IQueryable<Card> query, SortTypeEnum name);

       SortTypeEnum[] GetSortings();
    }
}
