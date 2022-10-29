﻿using System.ComponentModel.DataAnnotations.Schema;
using Yu_Gi_Oh_website.Models.BaseModels;

namespace Yu_Gi_Oh_website.Models.CardCatalogue.Models
{
    public class ExactCardType : BaseModel<int>
    {
        public ExactCardType()
        {
            this.Cards = new HashSet<Card>();
        }
        [Column(TypeName = "VARCHAR(100)")]
        public string Name { get; set; } = null!;

        public ICollection<Card> Cards { get; set; }
    }
}
