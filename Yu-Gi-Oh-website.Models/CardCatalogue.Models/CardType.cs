using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yu_Gi_Oh_website.Models.BaseModels;

namespace Yu_Gi_Oh_website.Models.CardCatalogue.Models
{
    public class CardType : BaseModel<int>
    {
        public CardType()
        {

            this.Cards = new HashSet<Card>();
        }

        [Column(TypeName = "VARCHAR(100)")]
        public string Name { get; set; } = null!;

        public ICollection<Card> Cards { get; set; }
    }
}
