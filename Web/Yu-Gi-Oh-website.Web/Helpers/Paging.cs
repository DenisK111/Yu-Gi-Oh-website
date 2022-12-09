using Yu_Gi_Oh_website.Web.Models.Contracts;

namespace Yu_Gi_Oh_website.Web.Helpers
{
    public static class Paging
    {
        public static void CreatePaging(IPagingModel model, int countOfPosts, int postsToTake, int Page)
        {
            var postsCount = countOfPosts;
            var pagesCount = (int)Math.Ceiling(postsCount / (decimal)postsToTake);

            if (model.Paging == null)
            {
                model.Paging = new();
            }

            model.Paging.CurrentPage = Page;
            model.Paging.PagesCount = pagesCount;
            model.Paging.ItemsCount = postsCount;           
        }

        public static int PageCheck(int Page)
        {
            if (Page < 1)
            {
                Page = 1;
            }

            return Page;
        }
    }
}
