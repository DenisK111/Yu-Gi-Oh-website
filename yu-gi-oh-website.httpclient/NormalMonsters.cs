using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yu_Gi_Oh_website.Models;

namespace yu_gi_oh_website
{
  
    public class NormalMonsters
    {
        public NormalMonsters()
        {
            this.CardImages = new HashSet<CardImage>();
        }
        [JsonIgnore]
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string Desc { get; set; } = null!;
        public ushort? Atk { get; set; }
        public ushort? Def { get; set; }

        public byte? Level { get; set; }
        public string? Race { get; set; } = null!;

        public string? Attribute { get; set; }

        public string? Archetype { get; set; }
        [JsonProperty("card_images")]
        public ICollection<CardImage> CardImages { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }



    }
}
