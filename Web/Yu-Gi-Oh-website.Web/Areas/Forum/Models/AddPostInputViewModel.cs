using Yu_Gi_Oh_website.Common.ValidationAttributes;

namespace Yu_Gi_Oh_website.Web.Areas.Forum.Models
{
	public class AddPostInputViewModel
	{
        [MaxLengthAfterRemovedHtml(5, 4000)]
        public string PostContent { get; set; } = null!;

        public string Author { get; set; } = null!;

        public int ThreadId { get; set; }
    }
}
