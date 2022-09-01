using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yu_Gi_Oh_website.Models.CardCatalogue.Models
{
    public class CardType
    {
        public CardType()
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
