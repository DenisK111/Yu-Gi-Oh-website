using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yu_Gi_Oh_website.Models.CardCatalogue.Models
{
    public class CardAttribute
    {
        public CardAttribute()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Cards = new HashSet<Card>();
        }
        
        public string Id { get; set; }
        [Column(TypeName = "VARCHAR(100)")]
        public string Name { get; set; } = null!;

        public ICollection<Card> Cards { get; set; }
    }
}