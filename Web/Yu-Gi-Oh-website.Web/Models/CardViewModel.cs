﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yu_Gi_Oh_website.Web.Models
{
    public class CardViewModel
    {

        public CardViewModel()
        {

            CardImages = new HashSet<string>();
        }
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string CardType { get; set; } = null!;

        public string ExactCardType { get; set; } = null!;

        public string Description { get; set; } = null!;
        public short? Atk { get; set; }
        public short? Def { get; set; }

        public byte? Level { get; set; }


        public string Type { get; set; } = null!;
        public string? CardAttribute { get; set; }
        public byte? Scale { get; set; }

        public byte? LinkValue { get; set; }
       
        public ICollection<string> CardImages { get; set; }
    }
}
