using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yu_Gi_Oh_website.Models.Enums;

namespace Yu_Gi_Oh_website.Services.ApiService

{
    internal static class ApiConstantValues
    {
        internal static readonly string allCardsString = $"https://db.ygoprodeck.com/api/v7/cardinfo.php?&startdate={DateTime.MinValue.Date.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)}&enddate={DateTime.UtcNow.Date.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)}&dateregion=tcg_date&format=tcg&misc=yes";

        internal static readonly Dictionary<CardTypeEnum, HashSet<string>> cardTypeMapping = new()
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

        internal static readonly HashSet<string> types = new HashSet<string>()
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



    }
}
