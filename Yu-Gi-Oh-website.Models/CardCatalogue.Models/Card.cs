using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yu_Gi_Oh_website.Models.BaseModels;
using Yu_Gi_Oh_website.Models.Enums;

namespace Yu_Gi_Oh_website.Models.CardCatalogue.Models
{
    public class Card : BaseModel<int>
    {
        public Card()
        {

            CardImages = new HashSet<CardImage>();
        }

        [MaxLength(200)]
        public string Name { get; set; } = null!;


        public string CardType { get; set; }

        [MaxLength(2000)]
        public string Description { get; set; } = null!;
        public short? Atk { get; set; }
        public short? Def { get; set; }

        public string? Level { get; set; }

        public CardType Type { get; set; } = null!;

        public int TypeId { get; set; }

        public CardAttribute? CardAttribute { get; set; }

        public int? CardAttributeId { get; set; }
        public byte? Scale { get; set; }

        public string? LinkValue { get; set; }

        public ICollection<CardImage> CardImages { get; set; }

        public int ExactCardTypeId { get; set; }

        public ExactCardType ExactCardType { get; set; } = null!;
    }
}
