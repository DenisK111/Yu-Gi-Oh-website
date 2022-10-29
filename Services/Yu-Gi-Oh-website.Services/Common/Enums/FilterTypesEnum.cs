using System.ComponentModel.DataAnnotations;

namespace Yu_Gi_Oh_website.Services.Common.Enums
{
    public enum FilterTypesEnum
    {
        [Display(Name = "Card Type")]
        Card_Type = 0,
        Attribute,
        [Display(Name = "Spell/Trap Type")]
        Spell_Trap_Type,
        Type,
        [Display(Name = "Level/Rank/Link")]
        Level_Rank_Link,
    }
}
