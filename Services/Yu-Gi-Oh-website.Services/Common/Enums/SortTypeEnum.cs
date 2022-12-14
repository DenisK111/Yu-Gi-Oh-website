using System.ComponentModel.DataAnnotations;

namespace Yu_Gi_Oh_website.Services.Common.Enums
{
    public enum SortTypeEnum
    {
       
        [Display(Name = "A-Z")]
        A_Z,
        [Display(Name = "Z-A")]
        Z_A,
        [Display(Name = "ATK ASCENDING")]
        ATKASC,
        [Display(Name = "ATK DESCENDING")]
        ATKDESC,
        [Display(Name = "LEVEL/RANK/LINK ASCENDING")]
        LEVEL_RANK_LINK_ASC,
        [Display(Name = "LEVEL/RANK/LINK DESCENDING")]
        LEVEL_RANK_LINK_DESC,
        [Display(Name = "DEF ASCENDING")]
        DEFASC,
        [Display(Name = "DEF DESCENDING")]
        DEFDESC,

    }
}
