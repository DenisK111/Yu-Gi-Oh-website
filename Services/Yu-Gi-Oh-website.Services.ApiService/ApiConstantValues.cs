using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yu_Gi_Oh_website.Services.ApiService

{ 
    internal static class ApiConstantValues
    {
        internal static readonly string allCardsString = $"https://db.ygoprodeck.com/api/v7/cardinfo.php?&startdate={DateTime.MinValue.Date.ToString("MM/dd/yyyy")}&enddate={DateTime.UtcNow.Date.ToString("MM/dd/yyyy")}&dateregion=tcg_date&misc=yes&format=tcg";
        internal static readonly HashSet<string> types = new HashSet<string>()
        {
            "Effect Monster",
            "Flip Effect Monster",
            "Flip Tuner Effect Monster",
            "Gemini Monster",
            "Normal Monster",
            "Normal Tuner Monster",
            "Pendulum Effect Monster",
            "Pendulum Flip Effect Monster",
            "Pendulum Normal Monster",
            "Pendulum Tuner Effect Monster",
            "Pendulum Effect Ritual Monster",
            "Ritual Effect Monster",
            "Ritual Monster",
            "Spell Card",
            "Spirit Monster",
            "Toon Monster",
            "Trap Card",
            "Tuner Monster",
            "Union Effect Monster",
            "Fusion Monster",
            "Link Monster",
            "Pendulum Effect Fusion Monster",
            "Synchro Monster",
            "Synchro Pendulum Effect Monster",
            "Synchro Tuner Monster",
            "XYZ Monster",
            "XYZ Pendulum Effect Monster",
        };

        internal static readonly HashSet<string> races = new HashSet<string>()
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
            "Normal",
            "Field",
            "Equip",
            "Continuous",
            "Quick-Play",
            "Ritual",
            "Normal",
            "Continuous",
            "Counter",
        };
              


    }
}
