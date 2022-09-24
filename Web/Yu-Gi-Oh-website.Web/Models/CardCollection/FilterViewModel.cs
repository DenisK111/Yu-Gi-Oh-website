using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Yu_Gi_Oh_website.Services.Common.Enums;
using Yu_Gi_Oh_website.Services.Models;

namespace Yu_Gi_Oh_website.Web.Models.CardCollection
{
    public class FilterViewModel
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]

        public string? SearchTerm { get; set; } = string.Empty;

        public Dictionary<FilterTypesEnum, List<FilterEntryModel>>? FilterEntries { get; set; }
        [FromQuery]
        public string[]? Filters { get; set; }

        [Range(1, int.MaxValue)]
        public int Page { get; set; }
        [EnumDataType(typeof(SortTypeEnum))]
        public SortTypeEnum Sorting { get; set; }

        public SortTypeEnum[]? Sortings { get; set; }

    }
}
