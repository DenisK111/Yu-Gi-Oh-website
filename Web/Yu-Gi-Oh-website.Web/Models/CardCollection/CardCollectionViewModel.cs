using Yu_Gi_Oh_website.Web.Models.Contracts;

namespace Yu_Gi_Oh_website.Web.Models.CardCollection
{
    public class CardCollectionViewModel : IPagingModel
    {
        public List<CardDisplayViewModel> CardModel { get; set; } = null!;

        public FilterViewModel Fm { get; set; } = null!;

        public PageViewModel Paging { get; set; } = null!;

    }
}
