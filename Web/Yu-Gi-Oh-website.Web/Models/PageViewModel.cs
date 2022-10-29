using System.ComponentModel.DataAnnotations;

namespace Yu_Gi_Oh_website.Web.Models
{
	public class PageViewModel
	{
        [Range(1, int.MaxValue)]
        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public int ItemsCount { get; set; }

        public int PreviousPage => CurrentPage == 1 ? 1 : CurrentPage - 1;

        public int NextPage => CurrentPage == PagesCount ? PagesCount : CurrentPage + 1;

        public string Url { get; set; } = null!;
    }
}
