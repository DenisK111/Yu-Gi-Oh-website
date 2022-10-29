using Yu_Gi_Oh_website.Services.Common.Enums;
using Yu_Gi_Oh_website.Services.Models;

namespace Yu_Gi_Oh_website.Services.Contracts
{
    public interface ICardCollectionService
    {
        Task<(IQueryable<CardDisplayDto>? cards,int count)> GetCardsAndCount(SortTypeEnum sortKey,string name,string[] parameters, bool applyFilter);
        Task<CardDto> GetCard(int Id);
    }
}
