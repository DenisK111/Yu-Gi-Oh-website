namespace Yu_Gi_Oh_website.Web.Models
{
    public class CardCollectionViewModel
    {
        public CardCollectionViewModel(List<CardDisplayViewModel> cardModel, FilterViewModel filterModel)
        {
            CardModel = cardModel;
            FilterModel = filterModel;
        }

        public List<CardDisplayViewModel> CardModel { get; set; }

        public FilterViewModel FilterModel { get; set; }
    }
}
