namespace Yu_Gi_Oh_website.Web.Areas.Forum.Models
{
	public class ThreadInfoViewModel
	{
        public int Id { get; set; }

        public int SubCattegoryId { get; set; }

        public string SubCattegorySlug { get; set; } = null!;              

        public int currentPage { get; set; }
    }
}
