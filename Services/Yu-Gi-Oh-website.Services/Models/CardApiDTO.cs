using Newtonsoft.Json;

namespace Yu_Gi_Oh_website.Services.ApiService.Models
{
    public class CardApiDTO
    {


        [JsonRequired]
        public string Name { get; set; } = null!;
        [JsonRequired]
        public string Type { get; set; } = null!;
        [JsonProperty("desc")]
        [JsonRequired]
        public string Description { get; set; } = null!;
        public short? Atk { get; set; }
        public short? Def { get; set; }

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
    [JsonProperty("question_atk")]
    public bool QuestionAtk { get; set; }
    [JsonProperty("question_def")]
    public bool QuestionDef { get; set; }
}



