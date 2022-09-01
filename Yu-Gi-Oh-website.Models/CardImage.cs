using System.ComponentModel.DataAnnotations;

namespace Yu_Gi_Oh_website.Models
{
    public class CardImage
    {
        public CardImage()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        [Key]
        public string Id { get; set; }

        public Card Card { get; set; } = null!;

        public string CardId { get; set; } = null!;

        [MaxLength(400)]
        public string ImageUrl { get; set; } = null!;
    }
}