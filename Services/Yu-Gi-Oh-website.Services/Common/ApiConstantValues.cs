using System.Globalization;
using Yu_Gi_Oh_website.Models.Enums;

namespace Yu_Gi_Oh_website.Services.ApiService

{
    public static class ApiConstantValues
    {
        public static readonly string imagePath = "wwwroot/Images";
        public static string placeholder = "placeholder";
        public static readonly string apiCallString =  $"https://db.ygoprodeck.com/api/v7/cardinfo.php?&startdate={placeholder}&enddate={DateTime.UtcNow.Date.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)}&dateregion=tcg_date&format=tcg&misc=yes";

        public static readonly Dictionary<CardTypeEnum, HashSet<string>> cardTypeMapping = new()
        {
            [CardTypeEnum.Normal] = new HashSet<string>()
            {
                "Normal Monster",
            "Normal Tuner Monster",
            "Pendulum Normal Monster",},
            [CardTypeEnum.Effect] = new HashSet<string>() {  "Effect Monster",
            "Flip Effect Monster",
            "Flip Tuner Effect Monster",
            "Gemini Monster",
            "Pendulum Effect Monster",
            "Pendulum Flip Effect Monster",
            "Pendulum Tuner Effect Monster",
            "Spirit Monster",
            "Toon Monster",
             "Tuner Monster",
            "Union Effect Monster",},
            [CardTypeEnum.Fusion] = new HashSet<string>() { "Fusion Monster", "Pendulum Effect Fusion Monster", },
            [CardTypeEnum.Synchro] = new HashSet<string>() { "Synchro Monster","Synchro Pendulum Effect Monster",
            "Synchro Tuner Monster", },
            [CardTypeEnum.Xyz] = new HashSet<string>() {    "XYZ Monster",
            "XYZ Pendulum Effect Monster",},

            [CardTypeEnum.Link] = new HashSet<string>() { "Link Monster", },
            [CardTypeEnum.Ritual] = new HashSet<string>() {   "Pendulum Effect Ritual Monster",
            "Ritual Effect Monster",
            "Ritual Monster", },
            [CardTypeEnum.Spell] = new HashSet<string>() { "Spell Card", },
            [CardTypeEnum.Trap] = new HashSet<string>() { "Trap Card", },
        };

        public static readonly HashSet<string> monsterTypes = new HashSet<string>()
        {
            "Aqua",
            "Beast",
            "Beast-Warrior",
            "Creator-God",
            "Cyberse",
            "Dinosaur",
            "Divine-Beast",
            "Dragon",
            "Fairy",
            "Fiend",
            "Fish",
            "Insect",
            "Machine",
            "Plant",
            "Psychic",
            "Pyro",
            "Reptile",
            "Rock",
            "Sea Serpent",
            "Spellcaster",
            "Thunder",
            "Warrior",
            "Winged Beast",
            "Wyrm",
            "Zombie",
           
        };

        public static readonly HashSet<string> spellTrapTypes = new HashSet<string>()
        {
             "Normal Spell",
            "Field Spell",
            "Equip Spell",
            "Continuous Spell",
            "Quick-Play Spell",
            "Ritual Spell",
            "Normal Trap",
            "Continuous Trap",
            "Counter Trap",
        };


        public static readonly HashSet<string> attributes = new HashSet<string>()
        {
            "LIGHT",
            "DARK",
            "WATER",
            "FIRE",
            "EARTH",
            "WIND",
            "DIVINE",

        };


    }
}
