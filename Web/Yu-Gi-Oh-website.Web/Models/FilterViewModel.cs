using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Yu_Gi_Oh_website.Services.Common.Enums;
using Yu_Gi_Oh_website.Services.Models;

namespace Yu_Gi_Oh_website.Web.Models
{
    public class FilterViewModel
    {
        [DisplayFormat(ConvertEmptyStringToNull = false)]

        public string? SearchTerm { get; set; } = String.Empty;

        public Dictionary<FilterTypesEnum, List<FilterEntryModel>>? FilterEntries { get; set; }
        [FromQuery]
        public string[]? Filters { get; set; }

        [Range(1, Int32.MaxValue)]
        public int Page { get; set; }

    }
}
