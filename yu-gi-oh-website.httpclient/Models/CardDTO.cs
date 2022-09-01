using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yu_gi_oh_website.httpclient.Models
{
    public class CardDTO
    {


        [JsonRequired]
        public string Name { get; set; } = null!;
        [JsonRequired]
        public string Type { get; set; } = null!;
        [JsonProperty("desc")]
        [JsonRequired]
        public string Description { get; set; } = null!;
        public ushort? Atk { get; set; }
        public ushort? Def { get; set; }

        public byte? Level { get; set; }
        [JsonRequired]
        public string Race { get; set; } = null!;


        public string? Attribute { get; set; }
        public byte? Scale { get; set; }
        [JsonProperty("linkval")]
        public byte? LinkValue { get; set; }
        [JsonProperty("misc_info")]
        public Misc[] Misc { get; set; } = null!;
        [JsonProperty("card_images")]
        public ICollection<CardImages> ImageUrls { get; set; } = null!;
    }
}

public class CardImages
{
    [JsonProperty("image_url")]
    public string? ImageUrl { get; set; }

}

public class Misc
{
    [JsonProperty("has_effect")]
    public bool HasEffect { get; set; }
}



