using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Yu_Gi_Oh_website.Models.BaseModels;
using Yu_Gi_Oh_website.Models.CardCatalogue.Models;

namespace Yu_Gi_Oh_website.Models
{
    public class CardImage : BaseModel<int>
    {

       
        

        public Card Card { get; set; } = null!;

        public int CardId { get; set; }

        [MaxLength(400)]
        public string ImageUrl { get; set; } = null!;
    }
}