using Yu_Gi_Oh_website.Services.Common.Enums;

namespace Yu_Gi_Oh_website.Web.Models.CardCollection
{
    public class CardCollectionViewModel
    {

        public List<CardDisplayViewModel> CardModel { get; set; } = null!;

        public FilterViewModel Fm { get; set; } = null!;



        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public int CardsCount { get; set; }

        public int PreviousPage => CurrentPage == 1 ? 1 : CurrentPage - 1;

        public int NextPage => CurrentPage == PagesCount ? PagesCount : CurrentPage + 1;


    }
}
