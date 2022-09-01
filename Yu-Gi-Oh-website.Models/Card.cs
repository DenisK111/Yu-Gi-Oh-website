using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yu_Gi_Oh_website.Models.Enums;

namespace Yu_Gi_Oh_website.Models
{
    public class Card
    {
        public Card()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CardImages = new HashSet<CardImage>();
        }
        public string Id { get; set; }


        [MaxLength(200)]
        public string Name { get; set; } = null!;
        public CardType Type { get; set; }
        [MaxLength(2000)]
        public string Description { get; set; } = null!;
        public ushort? Atk { get; set; }
        public ushort? Def { get; set; }

        public byte? Level { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "VARCHAR(100)")]
        public string? Race { get; set; } = null!;
        [Column(TypeName = "VARCHAR(100)")]
        public string? Attribute { get; set; }
       

        public byte? Scale { get; set; }

        public byte? LinkValue { get; set; }

        public bool HasEffect { get; set; }

        public ICollection<CardImage> CardImages { get; set; }

        

        
    }
}
