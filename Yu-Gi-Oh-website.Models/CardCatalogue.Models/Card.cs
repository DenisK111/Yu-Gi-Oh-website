using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Yu_Gi_Oh_website.Models.CardCatalogue.Models
{
    public class Card
    {
        public Card()
        {
            Id = Guid.NewGuid().ToString();
            CardImages = new HashSet<CardImage>();
        }
        public string Id { get; set; }


        [MaxLength(200)]
        public string Name { get; set; } = null!;

        public string CardTypeId { get; set; } = null!;
        public CardType CardType { get; set; } = null!;
        [MaxLength(2000)]
        public string Description { get; set; } = null!;
        public ushort? Atk { get; set; }
        public ushort? Def { get; set; }

        public byte? Level { get; set; }
        
       
        public Race Race { get; set; } = null!;

        public string RaceId { get; set; } = null!;

      
        
        public CardAttribute? CardAttribute { get; set; }

        public string? CardAttributeId { get; set; } 
        public byte? Scale { get; set; }

        public byte? LinkValue { get; set; }

        public bool HasEffect { get; set; }

        public ICollection<CardImage> CardImages { get; set; }




    }
}
