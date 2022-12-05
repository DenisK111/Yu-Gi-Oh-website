using Yu_Gi_Oh_website.Services.Forum.Models;

namespace Yu_Gi_Oh_website.Web.Areas.Administration.Models
{
    public class SubCattegoriesViewModel
    {
        public IEnumerable<SubCattegoryViewModel> SubCattegories { get; set; } = null!;
        public IEnumerable<CattegoryIdNameDto> Cattegories { get; set; } = null!;

    }
}
