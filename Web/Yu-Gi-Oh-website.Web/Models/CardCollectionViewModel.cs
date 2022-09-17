namespace Yu_Gi_Oh_website.Web.Models
{
    public class CardCollectionViewModel
    {

        public List<CardDisplayViewModel> CardModel { get; set; } = null!;

        public FilterViewModel Fm { get; set; } = null!;

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public int CardsCount { get; set; }

        public int PreviousPage => this.CurrentPage == 1 ? 1 : this.CurrentPage - 1;

        public int NextPage => this.CurrentPage == this.PagesCount ? this.PagesCount : this.CurrentPage + 1;

        
    }
}
