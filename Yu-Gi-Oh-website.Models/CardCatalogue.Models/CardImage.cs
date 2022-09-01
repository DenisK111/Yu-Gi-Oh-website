using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Yu_Gi_Oh_website.Models.CardCatalogue.Models;

namespace Yu_Gi_Oh_website.Models
{
    public class CardImage
    {

        public CardImage()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }

        public Card Card { get; set; } = null!;

        public string CardId { get; set; } = null!;

        [MaxLength(400)]
        public string ImageUrl { get; set; } = null!;
    }
}