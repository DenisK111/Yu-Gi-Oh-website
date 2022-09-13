using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yu_Gi_Oh_website.Models.CardCatalogue.Models;
using Yu_Gi_Oh_website.Services.Models;

namespace Yu_Gi_Oh_website.Services.Contracts
{
    public interface ICardCollectionService
    {
        Task<IEnumerable<CardDisplayDto>> GetCards(uint page, string name,string[] parameters, bool applyFilter);
        Task<CardDto> GetCard(int Id);
    }
}
